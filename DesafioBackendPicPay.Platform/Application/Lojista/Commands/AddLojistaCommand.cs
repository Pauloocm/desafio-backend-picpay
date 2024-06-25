namespace DesafioBackendPicPay.Platform.Application.Lojista.Commands
{
    public class AddLojistaCommand
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public required string Cnpj { get; set; }
    }
}
