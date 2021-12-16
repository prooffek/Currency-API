using System.Collections.Generic;
using AveneoRerutacja.Dimension;

namespace AveneoRerutacja.Domain
{
    public class DailyRate
    {
        public int Id { get; set; }
        public SourceCurrency SourceCurrency { get; }
        public TargetCurrency TargetCurrency { get; }
        public DateClass Date { get; }
        public decimal Rate { get; }
        

        //Setting EntityFramework relations foreign keys
        public ICollection<Currency> Currencies { get; set; }

        public DailyRate() { }

        public DailyRate(string date, decimal rate, string sourceCurrencyCode, string targetCurrencyCode, 
            string sourceCurrencyName = null, string targetCurrencyName = null)
        {
            SourceCurrency = new SourceCurrency(sourceCurrencyCode, sourceCurrencyName);
            TargetCurrency = new TargetCurrency(targetCurrencyCode, targetCurrencyName);
            Date = new DateClass(date);
            Rate = rate;
        }
        
        public DailyRate(DateClass date, decimal rate, SourceCurrency sourceCurrencies, TargetCurrency targetCurrency)
        {
            Date = date;
            Rate = rate;
            SourceCurrency = sourceCurrencies;
            TargetCurrency = targetCurrency;
        }
    }
}