﻿using System;
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
        private readonly DateClass _startDate;
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
            return await uow.DailyRates.GetAll(rate 
                    => rate.Date.Date >= Period.First().Date.Date && rate.Date.Date <= Period.Last().Date.Date);
        }

        public bool AllDailyRatesInDb()
        {
            return DailyRates.Count == Period.Count;
        }

        public async void AddDailyRatesToDb(IList<DailyRate> apiResult, IUnitOfWork<ExchangeRatesDbContext> uow)
        {
            try
            {
                var missingEntities = GetMissingEntities(apiResult);
                await uow.DailyRates.AddRange(missingEntities);
                await uow.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IList<DailyRate> GetMissingEntities(IList<DailyRate> apiResult)
        {
            List<DailyRate> entitiesMissingFromDb = new List<DailyRate>();
            entitiesMissingFromDb.AddRange(GetDailyRatesMissingFromDb(apiResult, entitiesMissingFromDb));
            

            foreach (var day in Period)
            {
                if (apiResult.Where(dr => dr.Date.Date == day.Date).ToList().Count <= 0)
                {
                    entitiesMissingFromDb.Add(new DailyRate(day, -1, apiResult.First().SourceCurrency, apiResult.First().TargetCurrency));
                }
            }

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