using System.Threading.Tasks;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;

namespace AveneoRerutacja.Infrastructure
{
    public interface IUnitOfWork
    {
        IGenericRepository<Currency> Currencies { get; }
        IGenericRepository<DateClass> Dates { get; }
        IGenericRepository<DailyRate> DailyRates { get; }
        Task Save();
    }
}