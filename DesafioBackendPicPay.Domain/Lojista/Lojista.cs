namespace DesafioBackendPicPay.Domain.Lojista
{
    public class Lojista : Entity<Guid>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public required string Cnpj { get; set; }

        public Lojista()
        {
            Id = Guid.NewGuid();
        }

        public void SetCnpj(string cnpj)
        {
            IsValid(cnpj);
        }

        private void IsValid(string cnpj)
        {
            ArgumentNullException.ThrowIfNull(nameof(cnpj));

            //TODO Implement CNPJ validation logic here

            Cnpj = cnpj;
        }
    }
}
