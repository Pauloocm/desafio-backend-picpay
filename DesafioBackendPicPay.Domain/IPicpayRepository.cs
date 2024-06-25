

using DesafioBackendPicPay.Domain.User;

namespace DesafioBackendPicPay.Domain
{
    public interface IPicpayRepository
    {
        Task Add(Lojista.Lojista lojista, CancellationToken cancellationToken = default);
        Task AddUser(User.User user, CancellationToken cancellationToken);
        Task<Entity<Guid>?> GetById(Guid sendById, CancellationToken cancellationToken = default);
        Task<Entity<Guid>?> GetReceivedById(Guid Id, CancellationToken cancellationToken = default);
    }
}
