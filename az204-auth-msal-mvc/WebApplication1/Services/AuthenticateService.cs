using Microsoft.Identity.Client;

namespace Services
{
    public interface IAuthenticateService
    {
        Task<string> Authenticate();
    }

    public class AuthenticateService : IAuthenticateService
    {
        private const string _clientId = "APPLICATION_CLIENT_ID";
        private const string _tenantId = "DIRECTORY_TENANT_ID";

        public async Task<string> Authenticate()
        {
            var app = PublicClientApplicationBuilder
                .Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost")
                .Build();

            string[] scopes = { "user.read" };
            var result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
            return result.AccessToken;
        }
    }
}
