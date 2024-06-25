using DesafioBackendPicPay.Domain;

namespace DesafioBackendPicPay.Platform.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        IPicpayRepository PicpayRepository { get; }

        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
        void Dispose();
    }
}
