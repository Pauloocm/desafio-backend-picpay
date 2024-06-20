namespace DesafioBackendPicPay.Domain.Exceptions
{
    public class InsufficientFundsException() : Exception("User funds is insufficiente for this transaction")
    {
    }
}
