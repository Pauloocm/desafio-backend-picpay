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

            var lojista = await unitOfWork.PicpayRepository.GetLojistaBy(command.Email, cancellationToken);

            if (lojista is not null) throw new LojistaAlreadyExistException();

            lojista = LojistaFactory.Create(command.FirstName, command.LastName, command.Email, command.Cnpj);

            await unitOfWork.PicpayRepository.Add(lojista, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return lojista.Id;
        }

        public async Task<Guid> AddUser(AddUserCommand command, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var user = await unitOfWork.PicpayRepository.GetUserBy(command.Email, cancellationToken);

            if (user is not null) throw new UserAlreadyExistException();

            user = UserFactory.Create(command.FirstName, command.LastName, command.Email, command.Cpf);

            await unitOfWork.PicpayRepository.AddUser(user, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return user.Id;
        }

        public async Task Transfer(TransferCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var sendedBy = await unitOfWork.PicpayRepository.GetById(command.SendById, cancellationToken) ??
                throw new UserNotFoundException(command.SendById);

            IsUserValid(sendedBy);

            var receivedBy = await unitOfWork.PicpayRepository.GetReceivedById(command.ReceivedById, cancellationToken) ??
                throw new ReceivedUserNotFoundException(command.ReceivedById);

            ValidateTransfer((Domain.User.User)sendedBy, receivedBy, command.Value);

            if (!await authorizationService.IsAuthorized()) throw new UnavelableOperationException();

            await unitOfWork.CommitAsync(cancellationToken);
        }

        private void ValidateTransfer(Domain.User.User sendedBy, Entity<Guid>? receivedBy, decimal value)
        {
            ArgumentNullException.ThrowIfNull(sendedBy, nameof(sendedBy));
            ArgumentNullException.ThrowIfNull(receivedBy, nameof(receivedBy));
            if (value <= 0) throw new ArgumentNullException(nameof(value));

            sendedBy.Debit(value);

            receivedBy.Deposit(value);
        }

        private void IsUserValid(Entity<Guid> entity)
        {
            if (entity.GetType().Name.Equals("Lojista")) throw new InvalidOperationException();
        }
    }
}
