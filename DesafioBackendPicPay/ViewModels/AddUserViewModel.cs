using DesafioBackendPicPay.Platform.Application.User.Commands;
using System.ComponentModel.DataAnnotations;

namespace DesafioBackendPicPay.ViewModels
{
    public class AddUserViewModel : BaseViewModel<AddUserCommand>
    {
        [StringLength(14, MinimumLength = 11)]
        public required string Cpf { get; set; }

        public override AddUserCommand ToCommand()
        {
            var command = new AddUserCommand()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Cpf = Cpf,
            };

            return command;
        }
    }
}
