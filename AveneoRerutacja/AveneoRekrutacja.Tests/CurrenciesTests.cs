using System;
using AveneoRerutacja.Dimension;
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
            var currency = new Currency(currencyCode);

            Assert.IsNotNull(currency.Code);
        }
        
        
        [TestCase("USDA")]
        [TestCase("US")]
        [TestCase("EU")]
        [TestCase("U")]
        [TestCase("")]
        public void ValidateCode_ShouldThrowArgumentExceptionIfInvalidLength(string currencyCode)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Currency(currencyCode));

            Assert.That(ex.Message, Is.EqualTo("Invalid currency code has been given."));
        }
        
        
        [TestCase(null)]
        public void ValidateCode_ShouldThrowArgumentNullExceptionIfNull(string currencyCode)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Currency(currencyCode));

            Assert.That(ex.ParamName, Is.EqualTo("Missing currency code(s)"));
        }
        
        
        [TestCase("USD")]
        [TestCase("EUR")]
        [TestCase("PLN")]
        public void ValidateCode_CodePropertyShouldBeAStringWithProperLength(string currencyCode, int expected = 3)
        {
            var currency = new Currency(currencyCode);

            var actual = currency.Code.Length;

            Assert.That(expected, Is.EqualTo(actual));
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
            var currency = new Currency(currencyCode);

            var actual = currency.Code;

            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}