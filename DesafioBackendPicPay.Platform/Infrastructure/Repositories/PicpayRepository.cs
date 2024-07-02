using DesafioBackendPicPay.Domain;
using DesafioBackendPicPay.Domain.Lojista;
using DesafioBackendPicPay.Domain.User;
using DesafioBackendPicPay.Platform.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackendPicPay.Platform.Infrastructure.Repositories
{
    public class PicpayRepository(DataContext context) : IPicpayRepository
    {
        private readonly DataContext dataContext = context ?? throw new ArgumentNullException(nameof(context));


        public async Task Add(Lojista lojista, CancellationToken cancellationToken = default)
        {
            await dataContext.Lojistas.AddAsync(lojista, cancellationToken);
        }

        public async Task AddUser(User user, CancellationToken cancellationToken)
        {
            await dataContext.Users.AddAsync(user, cancellationToken);
        }

        public async Task<Entity<Guid>?> GetById(Guid sendById, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(sendById));

            Entity<Guid>? entity;

            entity = await dataContext.Lojistas.FindAsync([sendById, cancellationToken], cancellationToken: cancellationToken);

            if (entity is null)
            {
                return await dataContext.Users.FindAsync([sendById, cancellationToken], cancellationToken: cancellationToken);
            }

            return entity;
        }

        public async Task<Lojista?> GetLojistaBy(string lojistaEmail, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(lojistaEmail)) throw new ArgumentNullException(lojistaEmail);

            var lojista = await dataContext.Lojistas.SingleOrDefaultAsync(l => l.Email == lojistaEmail, cancellationToken);

            return lojista;
        }

        public async Task<Entity<Guid>?> GetReceivedById(Guid Id, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(Id));

            Entity<Guid>? entity;

            entity = await dataContext.Lojistas.FindAsync([Id, cancellationToken], cancellationToken: cancellationToken);

            if (entity is null)
            {
                return await dataContext.Users.FindAsync([Id, cancellationToken], cancellationToken: cancellationToken);
            }

            return entity;
        }

        public async Task<User?> GetUserBy(string userEmail, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(userEmail)) throw new ArgumentNullException(userEmail);

            var user = await dataContext.Users.SingleOrDefaultAsync(l => l.Email == userEmail, cancellationToken);

            return user;
        }
    }
}
