namespace ExemplosDeIEnumerableEIQueryable;

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
        using var escopo = _serviceProvider.CreateScope();
        var exemploDeConsultaServico = escopo.ServiceProvider.GetRequiredService<IExemplosDeConsultaServico>();
        await exemploDeConsultaServico.ExecutarAsync(stoppingToken);
    }
}
