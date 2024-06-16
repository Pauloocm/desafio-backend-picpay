using DesafioBackendPicPay.Domain;
using DesafioBackendPicPay.Platform.Infrastructure.Database;

namespace DesafioBackendPicPay.Platform.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext context;

        public IPicpayRepository picpayRepository { get; private set; }

        public UnitOfWork(DataContext dataContext)
        {
            context = dataContext;
            picpayRepository = new PicpayRepository(context);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                await context.Database.RollbackTransactionAsync(cancellationToken);
            }


        }

        public void Dispose() => context.Dispose();
    }
}
