using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace AveneoRerutacja.ApiHandler
{
    public enum responseKeys
    {
        Date,
        ExchangeRate
    }
    
    public static class ApiDataGetter
    {
        public static ICollection<JToken> GetDataFromApiResponse(responseKeys key, JObject jsonData)
        {
            var responses = new Dictionary<responseKeys, Delegate>()
            {
                {
                    responseKeys.Date,
                    new Func<JObject, ICollection<JToken>>(json => 
                        json["structure"]["dimensions"]["observation"][0]["values"].Select(item => item.First.First)
                        .ToList())
                },
                {
                    responseKeys.ExchangeRate,
                    new Func<JObject, ICollection<JToken>>(json =>
                        json["dataSets"][0]["series"]["0:0:0:0:0"]["observations"].Select(item => item.First.First)
                            .ToList())
                }
            };

            return (ICollection<JToken>) responses[key].DynamicInvoke(jsonData);
        }
    }
}