namespace DesafioBackendPicPay.Domain.User
{
    public class User : Entity<Guid>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public required string Cpf;

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
