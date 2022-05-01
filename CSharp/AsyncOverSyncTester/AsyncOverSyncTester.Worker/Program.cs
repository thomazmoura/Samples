using AsyncOverSyncTester.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHttpClient<WeatherAPIService>();
    })
    .Build();

await host.RunAsync();
