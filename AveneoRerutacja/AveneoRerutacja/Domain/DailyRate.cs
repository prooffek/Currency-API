using System.Collections.Generic;
using AveneoRerutacja.Dimension;

namespace AveneoRerutacja.Domain
{
    public class DailyRate
    {
        public int Id { get; set; }
        public Currency SourceCurrency { get; }
        public Currency TargetCurrency { get; }
        public DateClass Date { get; }
        public decimal Rate { get; }

        
        //Setting EntityFramework relations foreign keys
        public ICollection<Currency> Currencies { get; set; }

        public DailyRate() { }

        public DailyRate(string date, decimal rate, string sourceCurrencyCode, string targetCurrencyCode, 
            string sourceCurrencyName = null, string targetCurrencyName = null)
        {
            SourceCurrency = new Currency(sourceCurrencyCode, sourceCurrencyName);
            TargetCurrency = new Currency(targetCurrencyCode, targetCurrencyName);
            Date = new DateClass(date);
            Rate = rate;
        }
    }
}