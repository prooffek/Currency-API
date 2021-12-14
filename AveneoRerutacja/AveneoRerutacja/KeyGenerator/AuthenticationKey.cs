using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AveneoRerutacja.KeyGenerator
{
    public class AuthenticationKey
    {
        public int Id { get; set; }
        public string KeyValue { get; set; }

        public AuthenticationKey()
        {
            KeyValue = GenerateKey();
        }

        private string GenerateKey()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public bool IsEqualTo(string externalKey)
        {
            if (string.IsNullOrEmpty(externalKey)) return false;

            return externalKey.Equals(this.KeyValue);
        }
    }
}
