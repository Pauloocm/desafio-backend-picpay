
namespace DesafioBackendPicPay.Domain.Lojista
{
    public static class LojistaFactory
    {
        public static Lojista Create(string firstName, string lastName, string email, string cpf)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
            ArgumentException.ThrowIfNullOrWhiteSpace(cpf, nameof(cpf));

            var lojista = new Lojista()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Cpf = cpf
            };

            return lojista;
        }
    }
}
