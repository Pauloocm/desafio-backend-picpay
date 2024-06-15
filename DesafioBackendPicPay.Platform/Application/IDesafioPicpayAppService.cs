using DesafioBackendPicPay.Platform.Application.Lojista.Commands;

namespace DesafioBackendPicPay.Platform.Application
{
    public interface IDesafioPicpayAppService
    {
        Task<Guid> Add(AddLojistaCommand command, CancellationToken cancellationToken = default);
    }
}
