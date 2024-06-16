namespace DesafioBackendPicPay.Domain.User
{
    public static class UserFactory
    {
        public static User Create(string firstName, string lastName, string email, string cpf)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
            ArgumentException.ThrowIfNullOrWhiteSpace(cpf, nameof(cpf));

            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Cpf = cpf
            };

            return user;
        }
    }
}
