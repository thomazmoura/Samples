IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<SystemConfig>(serviceProvider => new SystemConfig(args));
        services.AddHttpClient<ISampleAPIClient, SampleAPIClient>()
            .ConfigureHttpClient((serviceProvider, httpClient) => {
                var systemConfig = serviceProvider.GetService<SystemConfig>();
                var logger = serviceProvider.GetService<ILogger<ISampleAPIClient>>();
                var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                if(systemConfig is null || logger is null || httpClientFactory is null)
                    return;

                var authHttpClient = httpClientFactory.CreateClient();
                var discoveryEndpoint = authHttpClient.GetDiscoveryDocumentAsync(systemConfig.AuthorityURL).Result;
                if(discoveryEndpoint.IsError)
                    throw new Exception($"Unabled to contact {systemConfig.AuthorityURL}");

                var tokenResponse = authHttpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
                {
                    Address = discoveryEndpoint.TokenEndpoint,
                    ClientId = systemConfig.ClientName,
                    ClientSecret = systemConfig.ClientSecret,
                    Scope = systemConfig.ClientName
                }).Result;

                if(tokenResponse.IsError)
                    throw new Exception($"Unable to contact {systemConfig.AuthorityURL}");

                logger.LogInformation($"New token obtained from {systemConfig.AuthorityURL}");
                httpClient.SetBearerToken(tokenResponse.AccessToken);
            });
    })
    .Build();

await host.RunAsync();
