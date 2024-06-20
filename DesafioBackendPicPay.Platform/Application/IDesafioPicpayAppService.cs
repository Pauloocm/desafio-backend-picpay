using DesafioBackendPicPay.Platform.Application.Lojista.Commands;
using DesafioBackendPicPay.Platform.Application.User.Commands;

namespace DesafioBackendPicPay.Platform.Application
{
    public interface IDesafioPicpayAppService
    {
        Task<Guid> Add(AddLojistaCommand command, CancellationToken cancellationToken = default);
        Task<Guid> AddUser(AddUserCommand command, CancellationToken cancellationToken = default);
        Task Transfer(TransferCommand command, CancellationToken cancellationToken = default);
    }
}
