namespace DesafioBackendPicPay.Domain.User.User
{
    public class User : BaseUser<Guid>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        private string? Cpf;

        public void SetCpf(string cpf)
        {
            IsValid(cpf);
        }

        private void IsValid(string cpf)
        {
            ArgumentNullException.ThrowIfNull(nameof(cpf));

            // Implement CPF validation logic here

            Cpf = cpf;
        }
    }
}
