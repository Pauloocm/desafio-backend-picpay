using DesafioBackendPicPay.Domain;
using DesafioBackendPicPay.Domain.Exceptions;
using DesafioBackendPicPay.Domain.Lojista;
using DesafioBackendPicPay.Domain.User;
using DesafioBackendPicPay.Platform.Application.Lojista.Commands;
using DesafioBackendPicPay.Platform.Application.User.Commands;
using DesafioBackendPicPay.Platform.Infrastructure.Authorization;
using DesafioBackendPicPay.Platform.Infrastructure.Repositories;

namespace DesafioBackendPicPay.Platform.Application
{
    public class DesafioPicpayAppService(IUnitOfWork unitOfWorkContext, IAuthorizationService authorization) : IDesafioPicpayAppService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWorkContext;
        private readonly IAuthorizationService authorizationService = authorization;

        public async Task<Guid> Add(AddLojistaCommand command, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var lojista = LojistaFactory.Create(command.FirstName, command.LastName, command.Email, command.Cpf);

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

        public async Task Transfer(TransferCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var sendedBy = await unitOfWork.picpayRepository.GetById(command.SendById, cancellationToken) ??
                throw new UserNotFoundException(command.SendById);

            IsUserValid(sendedBy);

            var receivedBy = await unitOfWork.picpayRepository.GetById(command.ReceivedById, cancellationToken) ??
                throw new UserNotFoundException(command.ReceivedById);

            ValidateTransfer(sendedBy, receivedBy, command.Value);

            if (!await authorizationService.IsAuthorized()) throw new UnavelableOperationException();

            await unitOfWork.CommitAsync(cancellationToken);
        }

        private void ValidateTransfer(Entity<Guid> sendedBy, Entity<Guid>? receivedBy, decimal value)
        {
            ArgumentNullException.ThrowIfNull(sendedBy, nameof(sendedBy));
            ArgumentNullException.ThrowIfNull(receivedBy, nameof(receivedBy));
            if (value <= 0) throw new ArgumentNullException(nameof(value));

            if (sendedBy.Balance < value) throw new InsufficientFundsException();

            receivedBy.Balance += value;

            sendedBy.Balance -= value;
        }

        private void IsUserValid(Entity<Guid> entity)
        {
            if (entity.GetType().Name.Equals("Lojista")) throw new InvalidOperationException();
        }
    }
}
