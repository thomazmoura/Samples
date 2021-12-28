namespace AsyncOverSyncTester.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly WeatherAPIService _weatherAPIService;
    public Worker(ILogger<Worker> logger, WeatherAPIService weatherAPIService)
    {
        _logger = logger;
        _weatherAPIService = weatherAPIService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var currentTime = DateTime.Now;
        while (!stoppingToken.IsCancellationRequested)
        {
            Task.Run(() =>
            {
                _logger.LogInformation($"Calling weather API. Time since last execution: {DateTime.Now - currentTime}");

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var result = _weatherAPIService.CallWeatherForecastApiAndReturnResult();

                var worker = 0;
                var io = 0;
                ThreadPool.GetAvailableThreads(out worker, out io);

                stopwatch.Stop();
                _logger.LogInformation($"Request result = {result}. Worker threads: {worker}. I/O threads: {io}. Time Elapsed: {stopwatch.Elapsed} ");
            });
            Task.Delay(100, stoppingToken).GetAwaiter().GetResult();
            currentTime = DateTime.Now;
        }
    }
}

public class WeatherAPIService
{
    private readonly HttpClient _httpClient;
    public WeatherAPIService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string CallWeatherForecastApiAndReturnResult()
    {
        var actionResult = _httpClient.GetAsync("https://localhost:7223/weatherforecast").Result;
        return actionResult.Content.ReadAsStringAsync().Result;
    }
}
