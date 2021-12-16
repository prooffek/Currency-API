using AveneoRerutacja.ApiHandler;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using AveneoRerutacja.Data;
using AveneoRerutacja.DbHandler;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using AveneoRerutacja.Infrastructure;
using AveneoRerutacja.KeyGenerator;
using AveneoRerutacja.Models;
using Microsoft.AspNetCore.Authentication;

namespace AveneoRerutacja.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IUnitOfWork<ExchangeRatesDbContext> _erUnitOfWork;
        private readonly IUnitOfWork<AuthenticationKeyDbContext> _keyUnitOfWork;
        private readonly IMapper _mapper;
        private string _startDate;
        private string _endDate;

        public ExchangeRatesController(
            IUnitOfWork<ExchangeRatesDbContext> eruow, 
            IUnitOfWork<AuthenticationKeyDbContext> keyuow,
            IMapper mapper)
        {
            _erUnitOfWork = eruow;
            _keyUnitOfWork = keyuow;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetRates(string sourceCurrency = "USD", string targetCurrency = "EUR", 
            string startsOn = null, string endsOn = null, string apiKey = "abscd")
        {
            if (await AuthenticationKey.IsNotValid(_keyUnitOfWork, apiKey)) 
                return NotFound("Page not found");

            var (startDate, endDate) = DateClass.ValidateDates(startsOn, endsOn);
            
            var dbHandler = new DbRequestsHandler(startDate, endDate);
            dbHandler.DailyRates = await dbHandler.SetDailyRates(_erUnitOfWork);
            
            if (dbHandler.AllDailyRatesInDb())
                return Ok(_mapper.Map<IList<DailyRateDto>>(dbHandler.DailyRates));
            
            var client = ApiHelper.GetClient();

            using(HttpResponseMessage response = await client.GetAsync(
                ApiHelper.SetRequestUrl(
                    sourceCurrency, targetCurrency, startDate.ToString(), endDate.ToString(), apiKey
                    )))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    IDataGetter dataGetter = new JDataGetter(JObject.Parse(responseString));
                    IList<DailyRate> result = new ApiResponseHandler<IDataGetter>(dataGetter).GetDailyRates();
                    await dbHandler.AddDailyRatesToDb(result, _erUnitOfWork);

                    return Ok(_mapper.Map<IList<DailyRateDto>>(result));
                }
            }

            return Problem("Nothing found");
        }
    }
}
