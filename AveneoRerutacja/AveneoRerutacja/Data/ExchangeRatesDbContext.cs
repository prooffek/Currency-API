using AveneoRerutacja.Dimension;
using AveneoRerutacja.Domain;
using Microsoft.EntityFrameworkCore;

namespace AveneoRerutacja.Data
{
    public class ExchangeRatesDbContext : DbContext
    {
        public DbSet<SourceCurrency> SourceCurrencies { get; set; }
        public DbSet<TargetCurrency> TargetCurrencies { get; set; }
        public DbSet<DateClass> Dates { get; set; }
        public DbSet<DailyRate> DailyRates { get; set; }

        public ExchangeRatesDbContext(DbContextOptions<ExchangeRatesDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();
    }
}