namespace DesafioBackendPicPay.Platform.Application.User.Commands
{
    public class AddUserCommand
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Cpf { get; set; }
    }
}
