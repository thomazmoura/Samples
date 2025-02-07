namespace ExemplosDeSincronismo;

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

        var geradorDeDados = escopo.ServiceProvider.GetRequiredService<IGarantidorDeDados>();
        await geradorDeDados.GarantirQueHaDadosNaBase(stoppingToken);

        var exemploDeConsultaServico = escopo.ServiceProvider.GetRequiredService<IExemplosDeSincronismoServico>();
        await exemploDeConsultaServico.ExecutarAsync(stoppingToken);
    }
}
