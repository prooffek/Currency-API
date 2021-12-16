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
using AveneoRerutacja.Data;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using AveneoRerutacja.Infrastructure;
using AveneoRerutacja.KeyGenerator;
using Microsoft.AspNetCore.Authentication;

namespace AveneoRerutacja.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IUnitOfWork<ExchangeRatesDbContext> _erUnitOfWork;
        private readonly IUnitOfWork<AuthenticationKeyDbContext> _keyUnitOfWork;

        public ExchangeRatesController(IUnitOfWork<ExchangeRatesDbContext> eruow, IUnitOfWork<AuthenticationKeyDbContext> keyuow)
        {
            _erUnitOfWork = eruow;
            _keyUnitOfWork = keyuow;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetRates(string sourceCurrency = "USD", string targetCurrency = "EUR", string startDate = null, string endDate = null, string apiKey = "abscd")
        {
            var authenticationKey = await _keyUnitOfWork.AuthenticationKeys.Get(key => key.KeyValue == apiKey);
            if (authenticationKey == null) return NotFound("Page not found");
            
            var client = ApiHelper.GetClient();

            DateClass startsOn = new StartDate(startDate);
            DateClass endsOn = new EndDate(startsOn, endDate);
            
            using (HttpResponseMessage response = await client.GetAsync(ApiHelper.SetRequestUrl(sourceCurrency, targetCurrency, startsOn.ToString(), endsOn.ToString(), apiKey)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    IDataGetter dataGetter = new JDataGetter(JObject.Parse(responseString));

                    IList<DailyRate> dailyRates = new ApiResponseHandler<IDataGetter>(dataGetter).GetDailyRates();
                    
                    return Ok(dailyRates);
                }
            }

            return Problem("Nothing found");
        }
    }
}
