using System.Collections.Generic;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using Newtonsoft.Json.Linq;

namespace AveneoRerutacja.ApiHandler
{
    public interface IApiResponseHandleable
    {
        public IList<DailyRate> GetDailyRates();
    }
}