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
using Microsoft.AspNetCore.Routing;

namespace AveneoRerutacja.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IUnitOfWork<ExchangeRatesDbContext> _erUnitOfWork;
        private readonly IUnitOfWork<AuthenticationKeyDbContext> _keyUnitOfWork;
        private readonly IMapper _mapper;

        public ExchangeRatesController(
            IUnitOfWork<ExchangeRatesDbContext> eruow, 
            IUnitOfWork<AuthenticationKeyDbContext> keyuow,
            IMapper mapper)
        {
            _erUnitOfWork = eruow;
            _keyUnitOfWork = keyuow;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<ActionResult> GetRates([FromQuery] Dictionary<string, string> currencyCodes, 
            string startsOn, string endsOn, string apiKey)
        {
            if (await AuthenticationKey.IsValid(_keyUnitOfWork, apiKey))
            {
                try
                {
                    var (startDate, endDate) = DateClass.ValidateDates(startsOn, endsOn);

                    var dbHandler = new DbRequestsHandler(startDate.Copy(), endDate.Copy());
                    dbHandler.DailyRates = await dbHandler.SetDailyRates(_erUnitOfWork);

                    if (dbHandler.AllDailyRatesInDb())
                        return Ok(_mapper.Map<IList<DailyRateDto>>(dbHandler.DailyRates));

                    string responseString = await ApiHelper.GetResponseString(currencyCodes,
                        startDate.Copy(),
                        endDate.Copy(), apiKey);

                    IDataGetter dataGetter = new JDataGetter(responseString);
                    IList<DailyRate> result = new ApiResponseHandler<IDataGetter>(dataGetter).GetDailyRates();
                    await dbHandler.AddDailyRatesToDb(result, _erUnitOfWork);

                    return Ok(_mapper.Map<IList<DailyRateDto>>(result));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return NotFound("Page not found");
                }
            }
            
            return NotFound("Page not found");
        }
    }
}
