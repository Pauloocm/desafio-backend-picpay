namespace DesafioBackendPicPay.Domain.Exceptions
{
    public class UserNotFoundException(Guid id) :  Exception($"User with {id} was not found")
    {
    }
}
