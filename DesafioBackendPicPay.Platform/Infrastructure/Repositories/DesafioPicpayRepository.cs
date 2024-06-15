using DesafioBackendPicPay.Domain;
using DesafioBackendPicPay.Domain.Lojista;
using DesafioBackendPicPay.Platform.Infrastructure.Database;

namespace DesafioBackendPicPay.Platform.Infrastructure.Repositories
{
    public class DesafioPicpayRepository(DataContext context) : IDesafioPicpayRepository
    {
        private readonly DataContext dataContext = context ?? throw new ArgumentNullException(nameof(context));

        public async Task Add(Lojista lojista, CancellationToken cancellationToken = default)
        {
            await context.Lojistas.AddAsync(lojista, cancellationToken);
        }
    }
}
