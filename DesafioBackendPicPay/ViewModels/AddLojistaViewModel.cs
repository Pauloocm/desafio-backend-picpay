using DesafioBackendPicPay.Platform.Application.Lojista.Commands;
using System.ComponentModel.DataAnnotations;

namespace DesafioBackendPicPay.ViewModels
{
    public class AddLojistaViewModel : BaseViewModel<AddLojistaCommand>
    {
        [StringLength(18, MinimumLength = 14)]
        public required string Cnpj { get; set; }

        public override AddLojistaCommand ToCommand()
        {
            var command = new AddLojistaCommand()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Cnpj = Cnpj,
            };

            return command;
        }
    }
}
