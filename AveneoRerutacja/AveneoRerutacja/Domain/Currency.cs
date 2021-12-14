using System;

namespace AveneoRerutacja.Domain
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        private readonly int _codeLength;

        public Currency(string code, string name = null, int codeLength = 3)
        {
            _codeLength = codeLength;
            Code = ValidateCurrencyCode(code);
            Name = name;
        }

        private string ValidateCurrencyCode(string code)
        {
            if (code == null)
                throw new ArgumentNullException("Missing currency code(s)");

            if (code.Length != _codeLength)
                throw new ArgumentException("Invalid currency code has been given.");

            return code.ToUpper();
        }
    }
}