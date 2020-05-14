using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ClientCredentialsConnecter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string clientId, clientSecret, scope;
            if(args.Length >= 3)
            {
                clientId = args[0];
                clientSecret = args[1];
                scope = args[2];
            }
            else
            {
                Console.WriteLine("It's necessary to inform both client ID, client secret and scope");
                return;
            }

            var authHttpClient = new HttpClient();
            var discoveryEndpoint = await authHttpClient.GetDiscoveryDocumentAsync("https://www1.autotrac.com.br/ids");
            var tokenResponse = await authHttpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discoveryEndpoint.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scope
            });

            if(tokenResponse.IsError)
                Console.WriteLine($"It wasn't possible to connect with the authority. Detalhes: {tokenResponse.Error}");
            else
                Console.WriteLine(tokenResponse.AccessToken);
        }
    }
}
