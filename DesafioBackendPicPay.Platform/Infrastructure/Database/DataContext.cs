using DesafioBackendPicPay.Domain.Lojista;
using DesafioBackendPicPay.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackendPicPay.Platform.Infrastructure.Database
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=desafiopicpay;Username=postgres;Password=root",
                b => b.MigrationsAssembly("DesafioBackendPicPay.Migrations"));

            //optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Lojista> Lojistas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
