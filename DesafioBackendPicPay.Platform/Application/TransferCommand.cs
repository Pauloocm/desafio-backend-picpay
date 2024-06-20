namespace DesafioBackendPicPay.Platform.Application
{
    public class TransferCommand
    {
        public Guid SendById { get; set; }
        public Guid ReceivedById { get; set; }
        public decimal Value { get; set; }
    }
}
