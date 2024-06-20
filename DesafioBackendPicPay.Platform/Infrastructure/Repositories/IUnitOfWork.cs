using DesafioBackendPicPay.Domain;

namespace DesafioBackendPicPay.Platform.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        IPicpayRepository picpayRepository { get; }

        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
