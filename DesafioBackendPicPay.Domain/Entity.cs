namespace DesafioBackendPicPay.Domain
{
    public abstract class Entity<T> where T : struct
    {
        public T Id { get; set; }
        public decimal Balance { get; set; }
    }
}
