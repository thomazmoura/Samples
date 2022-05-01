public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();

            var sampleClient = scope.ServiceProvider.GetService<ISampleAPIClient>();
            var config = scope.ServiceProvider.GetService<SystemConfig>();
            if(config is null || config.APIUrl is null)
                throw new InvalidOperationException("SystemConfig is not injected");

            var resultado = await sampleClient.Get(config.APIUrl);
            if(resultado is not null)
                _logger.LogInformation($"O resultado foi: {resultado}");

            await Task.Delay(5000, stoppingToken);
        }
    }
}
