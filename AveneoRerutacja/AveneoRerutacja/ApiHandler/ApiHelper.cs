using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;

namespace AveneoRerutacja.ApiHandler
{
    public static class ApiHelper
    {
        private static HttpClient _httpClient;

        public static HttpClient GetClient(string headerValue = "application/json")
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://sdw-wsrest.ecb.europa.eu/");
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerValue));
            }

            return _httpClient;
        }

        public static string SetRequestUrl(Dictionary<string, string> currencyCodes, string startDate, 
            string endDate, string apiKey, string format = "jsondata")
        {
            string sourceCurrency = new List<string>(currencyCodes.Keys).FirstOrDefault() ?? "";
            string targetCurrency = currencyCodes[sourceCurrency];

            if (string.IsNullOrEmpty(sourceCurrency) || string.IsNullOrEmpty(targetCurrency))
                throw new NullReferenceException("Source or target currency is missing");
            
            string url = $"service/data/EXR/D.{sourceCurrency}.{targetCurrency}.SP00.A?startPeriod={startDate}&endPeriod={endDate}&format={format}";
            return url;
        }

        public static async Task<string> GetResponseString(Dictionary<string, string> currencyCodes, DateClass startDate, 
            DateClass endDate, string apiKey, string format = "jsondata")
        {
            GetClient();
            string responseString = null;
            
            //Counter prevents infinite loops when, e.g., the currencies like Euro were not used
            //in the requested period yet
            int counter = 20;

            do
            {
                using(HttpResponseMessage response = await _httpClient.GetAsync(SetRequestUrl(
                          currencyCodes, startDate.ToString(),
                              endDate.ToString(), apiKey
                          )))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                        startDate.SetToPreviousDay();
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new ArgumentException("Wrong argument provided");
                    }

                    counter--;
                }
            } while (string.IsNullOrEmpty(responseString) && counter >= 0);
            
            if (string.IsNullOrEmpty(responseString))
                throw new ArgumentException("No proper day found");

            return responseString;
        }
    }
}
