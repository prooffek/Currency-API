using System;
using System.Threading.Tasks;
using AveneoRerutacja.Data;
using AveneoRerutacja.Infrastructure;

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


        public static async Task<bool> IsValid(IUnitOfWork<AuthenticationKeyDbContext> uow, string apiKey)
        {
            var authenticationKey = await uow.AuthenticationKeys.Get(key => key.KeyValue == apiKey);
            return authenticationKey != null;
        }
    }
}
