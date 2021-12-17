using System;
using System.Net;
using System.Net.Http;
using AveneoRerutacja.Dimension;
using NUnit.Framework;

namespace AveneoRekrutacja.Tests
{
    [TestFixture]
    public class DateTests
    {
        [TestCase(null)]
        public void ValidateDate_ShouldThrowArgumentNullExceptionIfStartDateIsNull(string date)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new DateClass(date));

            Assert.That(ex.ParamName, Is.EqualTo("Invalid date provided."));
        }
        
        
        [TestCase("2009-05-01")]
        [TestCase("2021-12-01")]
        [TestCase("1998-01-31")]
        public void ValidateDate_ShouldSetTheDefaultEndDateToStartDateIfEndDateIsNull(string startDate)
        {
            var startsOn = new DateClass(startDate);
            var endsOn = new EndDate(startsOn, null);

            Assert.AreEqual(startsOn.Date, endsOn.Date);
        }
        
        
        [TestCase("2009-05-01")]
        [TestCase("2021-12-01")]
        [TestCase("1998-01-31")]
        public void ValidateDate_ShouldSetTheDefaultEndDateToStartDateIfOnlyOneDateGiven(string startDate)
        {
            var startsOn = new DateClass(startDate);
            var endsOn = new EndDate(startsOn);

            Assert.AreEqual(startsOn.Date, endsOn.Date);
        }
        
        
        [TestCase("2022-05-01", "2022-05-01")]
        [TestCase("2023-12-01", "2023-12-03")]
        [TestCase("3876-01-31", null)]
        public void ValidateDate_ShouldThrowNotFoundExceptionIfBothStartAndEndDateProceedTheTodaysDate(string startDate, string endDate)
        {
            var ex1 = Assert.Throws<HttpRequestException>( () => new DateClass(startDate));
            var ex2 = Assert.Throws<HttpRequestException>( () => new EndDate(new DateClass(startDate), endDate));
            
            Assert.That(ex1?.StatusCode == HttpStatusCode.NotFound);
            Assert.That(ex2?.StatusCode == HttpStatusCode.NotFound);
        }

        [TestCase("2021-12-04")]
        [TestCase("2021-12-05")]
        [TestCase("2010-01-02")]
        [TestCase("2010-01-03")]
        [TestCase("2009-05-09")]
        [TestCase("2009-05-10")]
        [TestCase("1998-02-14")]
        [TestCase("1998-02-15")]
        public void ValidateDate_ShouldModifyBothStartAndEndDateIfDatesAreSaturdayOrSunday(string date)
        {
            var weekDay = DateTime.Parse(date).DayOfWeek;

            var startOn = new DateClass(date);
            var endsOn = new EndDate(startOn);

            Assert.That(startOn.Date.DayOfWeek, Is.Not.EqualTo(weekDay));
            Assert.That(endsOn.Date.DayOfWeek, Is.Not.EqualTo(weekDay));
        }


        [TestCase("2021-12-04")]
        [TestCase("2021-12-05")]
        [TestCase("2010-01-02")]
        [TestCase("2010-01-03")]
        [TestCase("2009-05-09")]
        [TestCase("2009-05-10")]
        [TestCase("1998-02-14")]
        [TestCase("1998-02-15")]
        public void ValidateDate_NeitherStartNorEndDateShouldBeSaturdayOrSunday(string date)
        {
            var startsOn = new DateClass(date);
            var endsOn = new EndDate(startsOn);

            Assert.That(startsOn.Date.DayOfWeek, Is.Not.EqualTo(DayOfWeek.Saturday));
            Assert.That(startsOn.Date.DayOfWeek, Is.Not.EqualTo(DayOfWeek.Sunday));
            Assert.That(endsOn.Date.DayOfWeek, Is.Not.EqualTo(DayOfWeek.Saturday));
            Assert.That(endsOn.Date.DayOfWeek, Is.Not.EqualTo(DayOfWeek.Sunday));
        }


        [TestCase("2021-12-04")]
        [TestCase("2021-12-05")]
        [TestCase("2010-01-02")]
        [TestCase("2010-01-03")]
        [TestCase("2009-05-09")]
        [TestCase("2009-05-10")]
        [TestCase("1998-02-14")]
        [TestCase("1998-02-15")]
        public void ValidateDate_ShouldModifyOnlyStartDateIfStartDateIsWeekendAndEndDateIsAWorkingDay(string startDate)
        {
            var date = DateTime.Parse(startDate);
            var endDate = date.AddDays(3).ToString("yyyy-MM-dd");
            var weekDay = date.DayOfWeek;

            var startsOn = new DateClass(startDate);
            var endsOn = new EndDate(startsOn, endDate);

            Assert.That(startsOn.Date.DayOfWeek, Is.Not.EqualTo(weekDay));
            Assert.That(endsOn.Date.ToString("yyyy-MM-dd"), Is.EqualTo(endDate));
        }

        [TestCase("2021-12-04")]
        [TestCase("2021-12-05")]
        [TestCase("2010-01-02")]
        [TestCase("2010-01-03")]
        [TestCase("2009-05-09")]
        [TestCase("2009-05-10")]
        [TestCase("1998-02-14")]
        [TestCase("1998-02-15")]
        public void ValidateDate_ShouldModifyOnlyEndDateIfStartDateIsWeekendAndEndDateIsAWeekDay(string endDate)
        {
            var date = DateTime.Parse(endDate);
            var startDate = date.AddDays(-3).ToString("yyyy-MM-dd");
            var weekDay = date.DayOfWeek;

            var startsOn = new DateClass(startDate);
            var endsOn = new EndDate(startsOn, endDate);


            Assert.That(startsOn.Date.ToString("yyyy-MM-dd"), Is.EqualTo(startDate));
            Assert.That(endsOn.Date.DayOfWeek, Is.Not.EqualTo(weekDay));
        }

        [TestCase("2021-12-04")]
        [TestCase("2021-12-05")]
        [TestCase("2010-01-02")]
        [TestCase("2010-01-03")]
        [TestCase("2009-05-09")]
        [TestCase("2009-05-10")]
        [TestCase("1998-02-14")]
        [TestCase("1998-02-15")]
        public void ValidateDate_ShouldAssignFridayToStartDateIfDateIsSaturdayOrSunday(string date)
        {
            var startsOn = new DateClass(date);

            Assert.That(startsOn.Date.DayOfWeek, Is.EqualTo(DayOfWeek.Friday));
            Assert.That(startsOn.Date.ToString("yyyy-MM-dd"), Is.Not.EqualTo(date));
        }

        [TestCase("2021-12-04")]
        [TestCase("2021-12-05")]
        [TestCase("2010-01-02")]
        [TestCase("2010-01-03")]
        [TestCase("2009-05-09")]
        [TestCase("2009-05-10")]
        [TestCase("1998-02-14")]
        [TestCase("1998-02-15")]
        public void ValidateDate_ShouldAssignFridayToEndDateIfDateIsSaturdayOrSunday(string date)
        {
            var startDate = DateTime.Parse(date).AddDays(-3).ToString("yyyy-MM-dd");
            var startsOn = new DateClass(startDate);
            var endsOn = new EndDate(startsOn, date);

            Assert.That(endsOn.Date.DayOfWeek, Is.EqualTo(DayOfWeek.Friday));
            Assert.That(endsOn.Date.ToString("yyyy-MM-dd"), Is.Not.EqualTo(date));
        }


        // [TestCase("2009-05-10", "2009-05-10", "2009-05-11", "2009-05-12", "2009-05-13", "2009-05-14", "2009-05-15", true)]
        // [TestCase("2009-05-09", "2009-05-10", "2009-05-11", "2009-05-12", "2009-05-13", "2009-05-14", "2009-05-15", false)]
        // public void IsStartDateInTheList_ReturnsTrueIfTheDateIsInTheListElseReturnsFalse(string startDate, string date1, string date2, string date3, string date4, string date5, string date6, bool expected)
        // {
        //     var dates = new List<string> { date1, date2, date3, date4, date5, date6 };
        //     var timePeriod = new TimePeriod(startDate);
        //
        //     var actual = timePeriod.IsStartDateInTheList(dates);
        //
        //     Assert.That(actual, Is.EqualTo(expected));
        // }
        
    }
}