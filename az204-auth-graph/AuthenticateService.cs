using Microsoft.Identity.Client;

namespace az204_msal
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

            AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

            Console.WriteLine($"Token:\t{result.AccessToken}");
            return result.AccessToken;
        }
    }
}
