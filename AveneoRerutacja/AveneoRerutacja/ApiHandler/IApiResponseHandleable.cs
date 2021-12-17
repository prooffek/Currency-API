using System.Collections.Generic;
using AveneoRerutacja.Domain;

namespace AveneoRerutacja.ApiHandler
{
    public interface IApiResponseHandleable
    {
        public IList<DailyRate> GetDailyRates();
    }
}