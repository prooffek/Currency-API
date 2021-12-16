using System;
using System.Collections.Generic;
using System.Linq;
using AveneoRerutacja.Dimension;
using Newtonsoft.Json.Linq;

namespace AveneoRerutacja.ApiHandler
{
    public class JDataGetter : IDataGetter
    {
        private static JObject _json;

        public JDataGetter(string json)
        {
            try
            {
                _json = JObject.Parse(json);
            }
            catch (Exception e)
            {
                _json = null;
            }
        }

        public IList<string> GetData(ResponseKeys key)
        {
            return (IList<string>)ResponseData[key].DynamicInvoke();
        }

        public Dictionary<ResponseKeys, Delegate> ResponseData { get; } = new()
        {
            {
                ResponseKeys.Date,
                new Func<List<string>>(() => _json.SelectToken("structure.dimensions.observation[0].values")
                        .Select(item => item.First.First.ToString()).ToList())
            },
            {
                ResponseKeys.ExchangeRate,
                new Func<IList<string>>(() =>
                    _json.SelectToken("dataSets[0].series.0:0:0:0:0.observations")
                        .Select(item => item?.First?.First.ToString()).ToList())
            },
            {
                ResponseKeys.SourceCurrency,
                new Func<IList<string>>(() => 
                    _json.SelectToken("structure.dimensions.series[1].values[0]")
                        .Select(item => item?.First.ToString()).ToList())
            },
            {
                ResponseKeys.TargetCurrency,
                new Func<IList<string>>(() => 
                    _json.SelectToken("structure.dimensions.series[2].values[0]")
                        .Select(item => item?.First.ToString()).ToList())
            }
        };
    }
}