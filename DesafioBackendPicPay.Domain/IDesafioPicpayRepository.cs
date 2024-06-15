namespace DesafioBackendPicPay.Domain
{
    public interface IDesafioPicpayRepository
    {
        Task Add(Lojista.Lojista lojista, CancellationToken cancellationToken = default);
    }
}
