using System;
using System.Collections.Generic;
using System.Linq;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using Newtonsoft.Json.Linq;

namespace AveneoRerutacja.ApiHandler
{
    public enum ResponseKeys
    {
        Date,
        SourceCurrency,
        TargetCurrency,
        ExchangeRate,
    }

    enum CurrencyIndices
    {
        Code,
        Name
    }
    
    public class ApiResponseHandler<T> : IApiResponseHandleable where T : IDataGetter
    {
        private readonly IList<string> _dates;
        private readonly IList<string> _rates;
        private readonly IList<string> _sourceCurrency;
        private readonly IList<string> _targetCurrency;
        private readonly List<DailyRate> _dailyRates = new();

        public ApiResponseHandler(T response)
        {
            _dates = response.GetData(ResponseKeys.Date);
            _rates = response.GetData(ResponseKeys.ExchangeRate);
            _sourceCurrency = response.GetData(ResponseKeys.SourceCurrency);
            _targetCurrency = response.GetData(ResponseKeys.TargetCurrency);
        }
        public IList<DailyRate> GetDailyRates()
        {
            for (int i = 0; i < _dates.Count; i++)
            {
                _dailyRates.Add(new DailyRate(
                    _dates[i],
                    Convert.ToDecimal(_rates[i]),
                    _sourceCurrency[(int)CurrencyIndices.Code].ToString(),
                    _targetCurrency[(int)CurrencyIndices.Code].ToString(),
                    _sourceCurrency[(int)CurrencyIndices.Name].ToString(),
                    _targetCurrency[(int)CurrencyIndices.Name].ToString()
                ));
            }

            return _dailyRates;
        }
    }
}