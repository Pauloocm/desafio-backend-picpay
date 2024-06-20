using DesafioBackendPicPay.Domain.Lojista;
using DesafioBackendPicPay.Domain.User;
using DesafioBackendPicPay.Platform.Infrastructure.Database.Maps;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackendPicPay.Platform.Infrastructure.Database
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=desafiopicpay;Username=postgres;Password=root;Include Error Detail=True");

            //optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LojistaMap).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserMap).Assembly);
        }

        public DbSet<Lojista> Lojistas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
