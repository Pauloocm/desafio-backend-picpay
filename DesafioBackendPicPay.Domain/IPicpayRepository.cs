namespace DesafioBackendPicPay.Domain
{
    public interface IPicpayRepository
    {
        Task Add(Lojista.Lojista lojista, CancellationToken cancellationToken = default);
        Task AddUser(User.User user, CancellationToken cancellationToken);
        Task<Lojista.Lojista?> GetLojistaBy(string lojistaEmail, CancellationToken cancellationToken = default);
        Task<User.User?> GetUserBy(string userEmail, CancellationToken cancellationToken = default);
        Task<Entity<Guid>?> GetById(Guid sendById, CancellationToken cancellationToken = default);
        Task<Entity<Guid>?> GetReceivedById(Guid Id, CancellationToken cancellationToken = default);
    }
}
