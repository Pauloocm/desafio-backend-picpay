using DesafioBackendPicPay.Domain.Lojista;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioBackendPicPay.Platform.Infrastructure.Database.Maps
{
    public class LojistaMap : IEntityTypeConfiguration<Lojista>
    {
        public void Configure(EntityTypeBuilder<Lojista> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedNever();
            builder.Property(l => l.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(l => l.LastName).HasMaxLength(100).IsRequired();
            builder.Property(l => l.Balance);

            builder.HasIndex(l => l.Cpf).IsUnique();
            builder.HasIndex(l => l.Email).IsUnique();
        }
    }
}
