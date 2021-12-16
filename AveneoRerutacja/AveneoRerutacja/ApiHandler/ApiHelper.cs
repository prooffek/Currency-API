using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AveneoRerutacja.Dimension;

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

        public static string SetRequestUrl(string sourceCurrency, string targetCurrency, string startDate, string endDate, string apiKey, string format = "jsondata")
        {
            return $"service/data/EXR/D.{sourceCurrency}.{targetCurrency}.SP00.A?startPeriod={startDate}&endPeriod={endDate}&format={format}";
        }

        public static async Task<string> GetResponseString(string sourceCurrency, string targetCurrency, DateClass startDate, DateClass endDate, string apiKey, string format = "jsondata")
        {
            GetClient();
            string responseString = null;

            do
            {
                using(HttpResponseMessage response = await _httpClient.GetAsync(SetRequestUrl(
                          sourceCurrency, targetCurrency, startDate.ToString(),
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

                }
            } while (string.IsNullOrEmpty(responseString));

            return responseString;
        }
    }
}
