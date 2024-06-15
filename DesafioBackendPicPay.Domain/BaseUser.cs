namespace DesafioBackendPicPay.Domain
{
    public abstract class BaseUser<T> where T : struct
    {
        public T Id { get; set; }
    }
}
