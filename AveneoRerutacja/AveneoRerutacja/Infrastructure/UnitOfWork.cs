using System;
using System.Threading.Tasks;
using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using AveneoRerutacja.KeyGenerator;
using Microsoft.EntityFrameworkCore;

namespace AveneoRerutacja.Infrastructure
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        private readonly T _context;

        public UnitOfWork(T context)
        {
            _context = context;
        }

        public IGenericRepository<Currency> Currencies => new GenericRepository<Currency>(_context);
        
        public IGenericRepository<DateClass> Dates => new GenericRepository<DateClass>(_context);
        
        public IGenericRepository<DailyRate> DailyRates => new GenericRepository<DailyRate>(_context);

        public IGenericRepository<AuthenticationKey> AuthenticationKeys =>
            new GenericRepository<AuthenticationKey>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(true);
        }
        
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}