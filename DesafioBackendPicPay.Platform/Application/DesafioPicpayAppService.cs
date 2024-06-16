using DesafioBackendPicPay.Domain.Lojista;
using DesafioBackendPicPay.Domain.User;
using DesafioBackendPicPay.Platform.Application.Lojista.Commands;
using DesafioBackendPicPay.Platform.Application.User.Commands;
using DesafioBackendPicPay.Platform.Infrastructure.Repositories;

namespace DesafioBackendPicPay.Platform.Application
{
    public class DesafioPicpayAppService(IUnitOfWork unitOfWorkContext) : IDesafioPicpayAppService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWorkContext;

        public async Task<Guid> Add(AddLojistaCommand command, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var lojista = LojistaFactory.Create(command.FirstName, command.LastName, command.Email, command.Cpf);
            lojista.Id = Guid.Parse("9b289419-3ae1-474e-a212-34d3d9d74376");
            await unitOfWork.picpayRepository.Add(lojista, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return lojista.Id;
        }

        public async Task<Guid> AddUser(AddUserCommand command, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var user = UserFactory.Create(command.FirstName, command.LastName, command.Email, command.Cpf);

            await unitOfWork.picpayRepository.AddUser(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return user.Id;
        }
    }
}
