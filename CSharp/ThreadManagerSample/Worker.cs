namespace ThreadManagerSample;

public class Worker : BackgroundService
{
    public static int Count = 0;
    private readonly ILogger<Worker> _logger;
    private readonly List<Thread> threads = [];

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        for (var i = 0; i < 5; i++)
        {
            var thread = new Thread(thread =>
            {
                var objeto = thread as Dado;
                while (!stoppingToken.IsCancellationRequested && DateTime.Now < objeto.DeadLine)
                {
                    _logger.LogDebug("Running thread {} for the {} time until {}, {}", objeto.Nome, Count++, objeto.DeadLine, objeto.StoppingToken.IsCancellationRequested);
                    Thread.Sleep(1000);
                }
            });
            threads.Add(thread);
            thread.Name = $"Threadson {i}";
            var objeto = new Dado(thread.Name, DateTime.Now.AddSeconds(3 + (2 * i)), stoppingToken);
            thread.Start(objeto);
        }
        while (!stoppingToken.IsCancellationRequested)
        {
            if (threads.Any(thread => !thread.IsAlive))
            {
                _logger.LogWarning("First thread is dead");
                break;
            }
        }
        while (!stoppingToken.IsCancellationRequested)
        {
            if (threads.All(t => !t.IsAlive))
            {
                _logger.LogCritical("They are all dead now");
                break;
            }
        }
    }

    private record Dado(string Nome, DateTime DeadLine, CancellationToken StoppingToken);
}
