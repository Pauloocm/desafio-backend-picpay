namespace DesafioBackendPicPay.Domain.Exceptions
{
    public class InvalidDepositException(decimal value) : Exception($"Invalid deposit value: {value}. The deposit should be a value greater than zero")
    {
    }
}
