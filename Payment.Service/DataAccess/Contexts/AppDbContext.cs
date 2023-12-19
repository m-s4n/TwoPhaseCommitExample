
using Microsoft.EntityFrameworkCore;
using PaymentObject = Payment.Service.DataAccess.Entities.Payment;

namespace Payment.Service.DataAccess.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .UseSnakeCaseNamingConvention();
        }

        public DbSet<PaymentObject> Payments { get; set; }
    }
}
