using DesafioBackendPicPay.Domain.Exceptions;

namespace DesafioBackendPicPay.Domain
{
    public abstract class Entity<T> where T : struct
    {
        public T Id { get; set; }
        public decimal Balance { get; protected set; }

        public void Deposit(decimal value)
        {
            if (value <= 0) throw new InvalidDepositException(value);

            Balance += value;
        }
    }
}
