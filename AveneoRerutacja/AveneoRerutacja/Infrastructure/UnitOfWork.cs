using System.Threading.Tasks;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using Microsoft.EntityFrameworkCore;

namespace AveneoRerutacja.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Currency> Currencies => new GenericRepository<Currency>(_context);
        
        public IGenericRepository<DateClass> Dates => new GenericRepository<DateClass>(_context);
        
        public IGenericRepository<DailyRate> DailyRates => new GenericRepository<DailyRate>(_context);
        
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}