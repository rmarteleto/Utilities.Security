using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Utilities.Security.Test.App
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var client = new HttpClient();
            var discoveryDoc = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (discoveryDoc.IsError)
            {
                Console.WriteLine(discoveryDoc.Error);
                return;
            }

            // request client token only - https://identityserver4.readthedocs.io/en/latest/quickstarts/1_client_credentials.html
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDoc.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (response.IsError)
                Console.WriteLine(response.Error);
            else
                Console.WriteLine(response.Json);

            // request password token - https://identityserver4.readthedocs.io/en/latest/quickstarts/2_resource_owner_passwords.html
            response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryDoc.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "password",
                Scope = "api1"
            });

            if (response.IsError)
                Console.WriteLine(response.Error);
            else
                Console.WriteLine(response.Json);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

    }
}