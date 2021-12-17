using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AveneoRerutacja.Data;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using AveneoRerutacja.Infrastructure;

namespace AveneoRerutacja.DbHandler
{
    public class DbRequestsHandler
    {
        private DateClass _startDate;
        private readonly DateClass _endDate;
        
        public List<DateClass> Period { get; set; }
        
        public IList<DailyRate> DailyRates { get; set; }

        public DbRequestsHandler(DateClass startDate, DateClass endDate)
        {
            _startDate = startDate;
            _endDate = endDate;

            SetPeriod();
        }

        private void SetPeriod()
        {
            var temporaryList = new List<DateClass>();
            
            for (DateTime date = _startDate.Date; date <= _endDate.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    temporaryList.Add(new DateClass(date.ToString("yyyy-MM-dd")));
            }

            Period = temporaryList;
        }

        public async Task<IList<DailyRate>> SetDailyRates(IUnitOfWork<ExchangeRatesDbContext> uow)
        {
            //Get days of the given period saved in the db
            IList<DailyRate> days = await uow.DailyRates.GetAll(rate 
                    => rate.Date.Date >= Period.First().Date.Date && rate.Date.Date <= Period.Last().Date.Date && rate.Rate > 0,
                new List<string>() {"SourceCurrency", "TargetCurrency", "Date"});
            
            //Checks whether the first day in the list was not a holiday
            //If the day was a holiday, checks the previous day(s)
            //Db keeps holidays by assigning rate = -1
            DailyRate workingDay = days.Count > 0 && days.First().Date.Date == _startDate.Date && days?.First().Rate > 0 ? 
                null : await FindNearestWorkingDay(uow);
            
            if (workingDay != null) days.Add(workingDay);

            return days;
        }

        private async Task<DailyRate> FindNearestWorkingDay(IUnitOfWork<ExchangeRatesDbContext> uow)
        {
            //Rate == -1 means that the day was a holiday
            DailyRate day;
            do
            {
                _startDate.SetToPreviousDay();
                day = await uow.DailyRates.Get(rate => rate.Date.Date == _startDate.Date.Date,
                    new List<string>() {"SourceCurrency", "TargetCurrency", "Date"});
            } while (day?.Rate == -1);

            return day;
        }

        public bool AllDailyRatesInDb()
        {
            return DailyRates.Count == Period.Count;
        }

        public async Task AddDailyRatesToDb(IList<DailyRate> apiResult, IUnitOfWork<ExchangeRatesDbContext> uow)
        {
            var missingEntities = GetMissingEntities(apiResult);
            await uow.DailyRates.AddRange(missingEntities);
            await uow.Save();
        }

        private IList<DailyRate> GetMissingEntities(IList<DailyRate> apiResult)
        {
            List<DailyRate> entitiesMissingFromDb = new List<DailyRate>();
            entitiesMissingFromDb.AddRange(GetDailyRatesMissingFromDb(apiResult, entitiesMissingFromDb));
            entitiesMissingFromDb.AddRange(GetHolidayWeekDays(apiResult, entitiesMissingFromDb));

            return entitiesMissingFromDb;
        }
        
        private IList<DailyRate> GetDailyRatesMissingFromDb(IList<DailyRate> apiResult, IList<DailyRate> missingEntities)
        {
            foreach (var day in apiResult)
            {
                if (DailyRateIsMissing(DailyRates, day))
                    missingEntities.Add(day);
            }

            return missingEntities;
        }

        //The method draws on the fact that external Api does not return holidays
        //so if a date is missing from the response and it is neither Saturday nor Sunday
        //The bank was close on this day
        private IList<DailyRate> GetHolidayWeekDays(IList<DailyRate> apiResult, IList<DailyRate> missingEntities)
        {
            foreach (var day in Period)
            {
                if (DailyRateIsMissing(apiResult, day))
                {
                    missingEntities.Add(new DailyRate(day, -1, apiResult.First().SourceCurrency, apiResult.First().TargetCurrency));
                }
            }

            return missingEntities;
        }

        private bool DailyRateIsMissing(IList<DailyRate> source, DailyRate target)
            => source.Where(dr => dr.Date.Date == target.Date.Date).ToList().Count <= 0;
        
        private bool DailyRateIsMissing(IList<DailyRate> source, DateClass target)
            => source.Where(dr => dr.Date.Date == target.Date).ToList().Count <= 0;
    }
}