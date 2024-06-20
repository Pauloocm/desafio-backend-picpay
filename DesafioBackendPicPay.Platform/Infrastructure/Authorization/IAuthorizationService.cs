namespace DesafioBackendPicPay.Platform.Infrastructure.Authorization
{
    public interface IAuthorizationService
    {
        Task<bool> IsAuthorized();
    }
}
