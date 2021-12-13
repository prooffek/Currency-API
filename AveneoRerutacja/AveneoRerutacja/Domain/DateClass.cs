using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace AveneoRerutacja.Dimension
{
    public class DateClass
    {
        public int Id { get; set; }
        public DateTime Date { get; protected init; }

        public DateClass() { }
        
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

        public string GetDateString()
        {
            return Date.ToString("yyyy-MM-dd");
        }
    }
}