namespace DesafioBackendPicPay.Domain.Exceptions
{
    public class UnavelableOperationException() : Exception("The transfer operation is currently unavailable, please try again later")
    {
    }
}
