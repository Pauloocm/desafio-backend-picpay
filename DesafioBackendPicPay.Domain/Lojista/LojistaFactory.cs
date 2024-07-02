
namespace DesafioBackendPicPay.Domain.Lojista
{
    public static class LojistaFactory
    {
        public static Lojista Create(string firstName, string lastName, string email, string cnpj)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
            ArgumentException.ThrowIfNullOrWhiteSpace(cnpj, nameof(cnpj));

            var lojista = new Lojista()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Cnpj = cnpj
            };

            return lojista;
        }
    }
}
