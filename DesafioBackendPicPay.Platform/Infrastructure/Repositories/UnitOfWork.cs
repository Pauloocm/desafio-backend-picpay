using DesafioBackendPicPay.Domain;
using DesafioBackendPicPay.Platform.Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace DesafioBackendPicPay.Platform.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext context;
        private IDbContextTransaction transaction;

        public IPicpayRepository PicpayRepository { get; private set; }

        public UnitOfWork(DataContext dataContext)
        {
            context = dataContext;
            PicpayRepository = new PicpayRepository(context);
            transaction = context.Database.BeginTransaction();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await RollbackAsync(cancellationToken);
                throw;
            }
        }
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await transaction.RollbackAsync(cancellationToken);
            transaction.Dispose();
            transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        }

        public void Dispose()
        {
            transaction?.Dispose();
            context?.Dispose();
        }
    }
}
