using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace az204_keyvault
{
    public class KeyVaultService
    {
        public string Get(string chave)
        {
            // Create a new secret client using the default credential from Azure.Identity using environment variables previously set,
            // including AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, and AZURE_TENANT_ID.
            var keyVaultUrl = "";
            var client = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());

            // Retrieve a secret using the secret client.
            var secret = client.GetSecret(chave);

            return secret.ToString();
        }

        public void Set(string chave, string valor)
        {
            // Create a new secret client using the default credential from Azure.Identity using environment variables previously set,
            // including AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, and AZURE_TENANT_ID.
            var keyVaultUrl = "";
            var client = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());

            // Create a new secret using the secret client.
            KeyVaultSecret secret = client.SetSecret(chave, valor);
        }
    }
}