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
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;

namespace AveneoRerutacja.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRatesController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> GetRates(string sourceCurrency = "USD", string targetCurrency = "EUR", string startDate = null, string endDate = null, string apiKey = "")
        {
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
