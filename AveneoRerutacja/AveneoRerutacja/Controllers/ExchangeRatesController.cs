using AveneoRerutacja.ApiHandler;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

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

            if (startDate == null) startDate = "2009-05-06";
            if (endDate == null) endDate = startDate;


            using (HttpResponseMessage response = await client.GetAsync(ApiHelper.SetRequestUrl(sourceCurrency, targetCurrency, startDate, endDate, apiKey)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var dataInJson = JObject.Parse(data);

                    var dates = dataInJson["structure"]["dimensions"]["observation"][0]["values"].Select(item => item.First.First).ToList();
                    Console.WriteLine(dates[0]);
                }
            }
            return View();
        }
    }
}
