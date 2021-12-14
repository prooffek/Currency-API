using AveneoRerutacja.KeyGenerator;
using Microsoft.EntityFrameworkCore;

namespace AveneoRerutacja.Data
{
    public class AuthenticationKeyDbContext : DbContext
    {
        public DbSet<AuthenticationKey> Keys { get; set; }

        public AuthenticationKeyDbContext(DbContextOptions<AuthenticationKeyDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();
    }
}