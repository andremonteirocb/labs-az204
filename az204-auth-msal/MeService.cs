using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace az204_msal
{
    public interface IMeService
    {
        Task ReadMe(string accessToken);
    }

    public class MeService : IMeService
    {
        private const string _clientId = "7c88b15f-0c12-44fb-ae9c-867ebf7021c4";
        private const string _tenantId = "4a58a022-18c2-4856-8cca-b61ef4c56cd5";
         
        public async Task ReadMe(string accessToken)
        {
            string endpoint = "https://graph.microsoft.com/v1.0/me";

            // Create a new instance of HttpClient class
            var client = new HttpClient();

            // Build an auth header using your token
            var authHeader = new AuthenticationHeaderValue("Bearer", accessToken);

            // Set httpClient to use the previously-build auth header
            client.DefaultRequestHeaders.Authorization = authHeader;

            // Make a HTTP GET request to the endpoint
            var response = await client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<dynamic>();
                Console.WriteLine($"Graph me:\t{JsonSerializer.Serialize(json)}");
            }
        }
    }
}
