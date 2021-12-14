using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using Microsoft.EntityFrameworkCore;

namespace AveneoRerutacja.Data
{
    public class ExchangeRatesDbContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<DateClass> Dates { get; set; }
        public DbSet<DailyRate> DailyRates { get; set; }

        public ExchangeRatesDbContext(DbContextOptions<ExchangeRatesDbContext> options) : base(options)
        {
        }

        public ExchangeRatesDbContext(DbContextOptionsBuilder optionsBuilder) => 
            optionsBuilder.UseNpgsql();
    }
}