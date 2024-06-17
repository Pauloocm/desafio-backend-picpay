﻿using DesafioBackendPicPay.Domain;
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

        public async Task<Entity<Guid>?> GetById(Guid sendById, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(sendById));

            Entity<Guid>? entity;

            entity = await context.Lojistas.FindAsync([sendById, cancellationToken], cancellationToken: cancellationToken);

            if (entity is null)
            {
                return await context.Users.FindAsync([sendById, cancellationToken], cancellationToken: cancellationToken);
            }

            return entity;
        }
    }
}
