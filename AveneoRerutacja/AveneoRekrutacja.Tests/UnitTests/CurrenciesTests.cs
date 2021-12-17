using System;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using NUnit.Framework;

namespace AveneoRekrutacja.Tests
{
    [TestFixture]
    public class CurrenciesTests
    {
        [TestCase("USD")]
        public void ValidateCode_ShouldAddCorrectCurrrencyCodesToProperty(
            string currencyCode)
        {
            var currency1 = new SourceCurrency(currencyCode);
            var currency2 = new TargetCurrency(currencyCode);

            Assert.IsNotNull(currency1.Code);
            Assert.IsNotNull(currency2.Code);
        }
        
        
        [TestCase("USDA")]
        [TestCase("US")]
        [TestCase("EU")]
        [TestCase("U")]
        [TestCase("")]
        public void ValidateCode_ShouldThrowArgumentExceptionIfInvalidLength(string currencyCode)
        {
            var ex1 = Assert.Throws<ArgumentException>(() => new SourceCurrency(currencyCode));
            var ex2 = Assert.Throws<ArgumentException>(() => new TargetCurrency(currencyCode));

            Assert.That(ex1.Message, Is.EqualTo("Invalid currency code has been given."));
            Assert.That(ex2.Message, Is.EqualTo("Invalid currency code has been given."));
        }
        
        
        [TestCase(null)]
        public void ValidateCode_ShouldThrowArgumentNullExceptionIfNull(string currencyCode)
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() => new SourceCurrency(currencyCode));
            var ex2 = Assert.Throws<ArgumentNullException>(() => new TargetCurrency(currencyCode));

            Assert.That(ex1.ParamName, Is.EqualTo("Missing currency code(s)"));
            Assert.That(ex2.ParamName, Is.EqualTo("Missing currency code(s)"));
        }
        
        
        [TestCase("USD")]
        [TestCase("EUR")]
        [TestCase("PLN")]
        public void ValidateCode_CodePropertyShouldBeAStringWithProperLength(string currencyCode, int expected = 3)
        {
            var currency1 = new SourceCurrency(currencyCode);
            var currency2 = new TargetCurrency(currencyCode);

            var actual1 = currency1.Code.Length;
            var actual2 = currency2.Code.Length;

            Assert.That(expected, Is.EqualTo(actual1));
            Assert.That(expected, Is.EqualTo(actual2));
        }
        
        
        [TestCase("usd", "USD")]
        [TestCase("Usd", "USD")]
        [TestCase("uSd", "USD")]
        [TestCase("usD", "USD")]
        [TestCase("USd", "USD")]
        [TestCase("uSD", "USD")]
        [TestCase("UsD", "USD")]
        [TestCase("USD", "USD")]
        public void ValidateCode_CurrencyCodePropertyValueShouldBeInUpperCases(string currencyCode,
            string expected)
        {
            var currency1 = new SourceCurrency(currencyCode);
            var currency2 = new TargetCurrency(currencyCode);

            var actual1 = currency1.Code;
            var actual2 = currency2.Code;

            Assert.That(expected, Is.EqualTo(actual1));
            Assert.That(expected, Is.EqualTo(actual2));
        }
    }
}