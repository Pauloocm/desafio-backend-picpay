namespace DesafioBackendPicPay.Domain.Exceptions
{
    public class InvalidDebitException(decimal value) : Exception($"Invalid debit value: {value}. The debit should be a value greater than zero")
    {
    }
}
