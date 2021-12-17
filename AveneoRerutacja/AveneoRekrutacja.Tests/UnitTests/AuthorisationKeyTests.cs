using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using AveneoRerutacja.KeyGenerator;

namespace AveneoRekrutacja.Tests
{
    [TestFixture]
    class AuthorisationKeyTests
    {

        [Test]
        public void AuthenticationKey_ShouldCreateAnAuthenticationKeyObjectWithNotNullKeyValueAtribute()
        {
            var key = new AuthenticationKey();

            Assert.That(key.KeyValue, Is.Not.Null);
        }

        [Test]
        public void AuthenticationKey_ShouldCreateAnAuthenticationKeyObjectWithNotEmptyKeyValueAttribute()
        {
            var key = new AuthenticationKey();

            Assert.That(key.KeyValue, Is.Not.Empty);
        }

        [Test]
        public void AuthenticationKey_ShouldCreateAuthenticationKeyObjectsWithDifferentKeyValueAttributeValues()
        {
            var key_1 = new AuthenticationKey();
            var key_2 = new AuthenticationKey();

            Assert.That(key_1.KeyValue, Is.Not.EqualTo(key_2.KeyValue));
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        [TestCase(10000000)]
        public void AuthenticationKey_ShouldCreateAuthenticationKeyObjectsWithUniqueKeyValueAttributeValue(int counter)
        {
            List<string> keyValues = PopulateAuthenticationKeyValuesList(counter);

            Assert.That(keyValues, Is.Unique);
        }

        private List<string> PopulateAuthenticationKeyValuesList(int counter)
        {
            List<string> keyValues = new();

            for (int i = 0; i < counter; i++)
            {
                keyValues.Add(new AuthenticationKey().KeyValue);
            }

            return keyValues;
        }
    }
}
