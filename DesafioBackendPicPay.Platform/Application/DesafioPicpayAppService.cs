using DesafioBackendPicPay.Domain;
using DesafioBackendPicPay.Domain.Lojista;
using DesafioBackendPicPay.Platform.Application.Lojista.Commands;

namespace DesafioBackendPicPay.Platform.Application
{
    public class DesafioPicpayAppService : IDesafioPicpayAppService
    {
        private readonly IDesafioPicpayRepository picpayRepository;

        public async Task<Guid> Add(AddLojistaCommand command, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var lojista = LojistaFactory.Create(command.FirstName, command.LastName, command.Email, command.Cpf);

            await picpayRepository.Add(lojista, cancellationToken);
            //await unitOfWork.CommitAsync(cancellationToken);

            return lojista.Id;
        }
    }
}
