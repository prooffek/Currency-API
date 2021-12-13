using AveneoRerutacja.ApiHandler;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AveneoRerutacja.Dimension;

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

            DateClass startsOn = new DateClass(startDate);
            DateClass endsOn = new EndDate(startsOn, endDate);


            using (HttpResponseMessage response = await client.GetAsync(ApiHelper.SetRequestUrl(sourceCurrency, targetCurrency, startsOn.GetDateString(), endsOn.GetDateString(), apiKey)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var dataInJson = JObject.Parse(data);

                    var dates = dataInJson["structure"]["dimensions"]["observation"][0]["values"].Select(item => item.First.First).ToList();
                    var rates = dataInJson["dataSets"][0]["series"]["0:0:0:0:0"]["observations"].Select(item => item.First.First).ToList();
                    Console.WriteLine(dates[0]);
                }
            }
            return Ok();
        }
    }
}
