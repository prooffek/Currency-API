using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AveneoRerutacja.Domain;

namespace AveneoRerutacja.Dimension
{
    public class DateClass
    {
        public int Id { get; set; }
        public DateTime Date { get; protected set; }
        
        
        //Setting EntityFramework relations foreign keys
        public ICollection<DailyRate> DailyRates { get; set; }

        protected DateClass() { }
        
        public DateClass(string date)
        {
            Date = ValidateDate(date);
        }

        protected virtual DateTime ValidateDate(string date)
        {
            var dateTime = ConvertToDateTime(date);
            dateTime = HandleWeekendDays(dateTime);
            return HandleFutureDates(dateTime);
        }

        private DateTime ConvertToDateTime(string date)
        {
            try
            {
                return DateTime.Parse(date);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentNullException($"Invalid date provided.");
            }
        }
        
        private DateTime HandleWeekendDays(DateTime date)
        {
            Dictionary<DayOfWeek, int> daysToFriday = new()
            {
                { DayOfWeek.Saturday, -1 },
                { DayOfWeek.Sunday, -2 }
            };

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) 
                date = date.AddDays(daysToFriday[date.DayOfWeek]);

            return date;
        }

        private DateTime HandleFutureDates(DateTime date)
        {
            var today = DateTime.Today;

            if (today >= date)
                return date;

            throw new HttpRequestException("Defined period has not been found", null, HttpStatusCode.NotFound);
        }

        public override string ToString()
        {
            return Date.ToString("yyyy-MM-dd");
        }

        public static (DateClass, DateClass) ValidateDates(string date1, string date2)
        {
            var startDate = new StartDate(date1);
            var endDate = new EndDate(startDate, date2);

            return (startDate, endDate);
        }

        public void SetToPreviousDay()
        {
            Date = Date.AddDays(-1);
        }

        public DateClass Copy()
        {
            return new DateClass(Date.ToString("yyyy-MM-dd"));
        }
    }
}