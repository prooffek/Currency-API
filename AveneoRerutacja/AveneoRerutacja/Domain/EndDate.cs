using System;
using AveneoRerutacja.Util;

namespace AveneoRerutacja.Dimension
{
    public class EndDate : DateClass
    {
        private readonly DateClass _startDate;
        
        public EndDate(DateClass startDate, string date = null) : base()
        {
            _startDate = startDate;
            Date = ValidateDate(date);
        }

        protected override DateTime ValidateDate(string date)
        {
            if (string.IsNullOrEmpty(date) && _startDate != null)
                return _startDate.Date;
            
            return base.ValidateDate(date);
        }
    }
}