namespace DesafioBackendPicPay.Domain.Exceptions
{
    public class ReceivedUserNotFoundException(Guid id) :  Exception($"User with {id} was not found")
    {
    }
}
