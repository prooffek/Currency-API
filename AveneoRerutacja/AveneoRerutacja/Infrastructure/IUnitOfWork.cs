using System.Threading.Tasks;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using AveneoRerutacja.KeyGenerator;
using Microsoft.EntityFrameworkCore;

namespace AveneoRerutacja.Infrastructure
{
    public interface IUnitOfWork<T> where T : DbContext
    {
        IGenericRepository<Currency> Currencies { get; }
        IGenericRepository<DateClass> Dates { get; }
        IGenericRepository<DailyRate> DailyRates { get; }
        IGenericRepository<AuthenticationKey> AuthenticationKeys { get; }
        Task Save();
    }
}