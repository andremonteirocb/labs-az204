using Microsoft.Identity.Client;

namespace az204_msal
{
    public interface IAuthenticateService
    {
        Task<string> Authenticate();
    }

    public class AuthenticateService : IAuthenticateService
    {
        private const string _clientId = "7c88b15f-0c12-44fb-ae9c-867ebf7021c4";
        private const string _tenantId = "4a58a022-18c2-4856-8cca-b61ef4c56cd5";
         
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
