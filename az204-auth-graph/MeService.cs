using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace az204_msal
{
    public interface IMeService
    {
        Task ReadDelegateAuthenticationProvider();
        Task ReadInteractiveBrowser();
        Task ReadUsernamePassword();
        Task ReadDeviceCode();
    }

    public class MeService : IMeService
    {
        private const string _clientId = "a6e232a8-6746-4c07-b0a4-d60cfe642c0a";
        private const string _tenantId = "4a58a022-18c2-4856-8cca-b61ef4c56cd5";

        /// <summary>
        /// Obtém o token através do browser MSAL
        /// </summary>
        /// <returns></returns>
        public async Task ReadDelegateAuthenticationProvider()
        {
            Console.WriteLine("Processando Provider delegate MSAL.");

            var scopes = new[] { "User.Read" };
            var app = PublicClientApplicationBuilder
                .Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost")
                .Build();

            // DelegateAuthenticationProvider is a simple auth provider implementation
            // that allows you to define an async function to retrieve a token
            // Alternatively, you can create a class that implements IAuthenticationProvider
            // for more complex scenarios
            var authProvider = new DelegateAuthenticationProvider(async (request) => {
                // Use Microsoft.Identity.Client to retrieve token
                var result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
            });

            var graphClient = new GraphServiceClient(authProvider);
            var user = await graphClient.Me
                .Request()
                .GetAsync();

            Console.WriteLine($"Graph Me:\t{user.DisplayName}");
        }

        /// <summary>
        /// Obtém o token através do browser autenticando com sua conta
        /// </summary>
        /// <returns></returns>
        public async Task ReadInteractiveBrowser()
        {
            Console.WriteLine("Processando Interactive Browser.");

            var scopes = new[] { "User.Read" };
            var options = new InteractiveBrowserCredentialOptions
            {
                TenantId = _tenantId,
                ClientId = _clientId,
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                // MUST be http://localhost or http://localhost:PORT
                // See https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/System-Browser-on-.Net-Core
                RedirectUri = new Uri("http://localhost"),
            };

            // https://docs.microsoft.com/dotnet/api/azure.identity.interactivebrowsercredential
            var interactiveCredential = new InteractiveBrowserCredential(options);

            var graphClient = new GraphServiceClient(interactiveCredential, scopes);
            var user = await graphClient.Me
                .Request()
                .GetAsync();

            Console.WriteLine($"Graph Me:\t{user.DisplayName}");
        }

        /// <summary>
        /// Obtém o token através de um login e senha
        /// </summary>
        /// <returns></returns>
        public async Task ReadUsernamePassword()
        {
            Console.WriteLine("Processando Usuário/Senha.");

            var scopes = new[] { "User.Read" };

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var userName = "locatario@andremonteirocbhotmail.onmicrosoft.com";
            var password = "Nin@0208";

            // https://docs.microsoft.com/dotnet/api/azure.identity.usernamepasswordcredential
            var userNamePasswordCredential = new UsernamePasswordCredential(
                userName, password, _tenantId, _clientId, options);

            var graphClient = new GraphServiceClient(userNamePasswordCredential, scopes);
            var user = await graphClient.Me
                .Request()
                .GetAsync();

            Console.WriteLine($"Graph Me:\t{user.DisplayName}");
        }

        /// <summary>
        /// Obtém o token através do browser informando o device code
        /// </summary>
        /// <returns></returns>
        public async Task ReadDeviceCode()
        {
            Console.WriteLine("Processando Device Code.");

            var scopes = new[] { "User.Read" };
            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // Callback function that receives the user prompt
            // Prompt contains the generated device code that use must
            // enter during the auth process in the browser
            Func<DeviceCodeInfo, CancellationToken, Task> callback = (code, cancellation) => {
                Console.WriteLine(code.Message);
                return Task.FromResult(0);
            };

            // https://docs.microsoft.com/dotnet/api/azure.identity.devicecodecredential
            var deviceCodeCredential = new DeviceCodeCredential(
                callback, _tenantId, _clientId, options);

            var graphClient = new GraphServiceClient(deviceCodeCredential, scopes);
            var user = await graphClient.Me
                .Request()
                .GetAsync();

            Console.WriteLine($"Graph Me:\t{user.DisplayName}");
        }
    }
}
