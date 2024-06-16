using DesafioBackendPicPay.Domain;
using DesafioBackendPicPay.Domain.Lojista;
using DesafioBackendPicPay.Domain.User;
using DesafioBackendPicPay.Platform.Infrastructure.Database;

namespace DesafioBackendPicPay.Platform.Infrastructure.Repositories
{
    public class PicpayRepository(DataContext context) : IPicpayRepository
    {
        private readonly DataContext dataContext = context ?? throw new ArgumentNullException(nameof(context));


        public async Task Add(Lojista lojista, CancellationToken cancellationToken = default)
        {
            await context.Lojistas.AddAsync(lojista, cancellationToken);
        }

        public async Task AddUser(User user, CancellationToken cancellationToken)
        {
            await context.Users.AddAsync(user, cancellationToken);
        }
    }
}
