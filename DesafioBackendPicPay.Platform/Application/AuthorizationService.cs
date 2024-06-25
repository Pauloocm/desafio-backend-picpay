using DesafioBackendPicPay.Platform.Infrastructure.Authorization;

namespace DesafioBackendPicPay.Platform.Application
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient httpClient;
        private readonly string Address = "https://util.devi.tools/api/v2/authorize";

        public AuthorizationService()
        {
            httpClient = new HttpClient();
        }


        public async Task<bool> IsAuthorized()
        {

            var result = await httpClient.GetAsync(Address);

            var res = await result.Content.ReadAsStringAsync();

            if(res.Contains("fail") || res.Contains("false"))
                return false;

            return true;
        }
    }
}
