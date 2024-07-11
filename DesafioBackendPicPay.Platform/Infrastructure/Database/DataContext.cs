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
            //TODO add db address
            //optionsBuilder.UseNpgsql("Server=Include Error Detail=True");

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
