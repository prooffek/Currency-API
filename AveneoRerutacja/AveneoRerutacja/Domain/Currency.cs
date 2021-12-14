using System;

namespace AveneoRerutacja.Dimension
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }

        private readonly int _codeLength;

        public Currency(string code, int codeLength = 3)
        {
            _codeLength = codeLength;
            Code = ValidateCurrencyCode(code);
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