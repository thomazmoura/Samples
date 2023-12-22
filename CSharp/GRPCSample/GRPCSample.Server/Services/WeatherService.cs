namespace GRPCSample.Server.Services;

public class WeatherService: Weather.WeatherBase
{
    private readonly ILogger<WeatherService> _logger;
    public WeatherService(ILogger<WeatherService> logger)
    {
        _logger = logger;
    }

    public override Task<WeatherReply> GetWeather(WeatherRequest request, ServerCallContext context)
    {
        var reply = new WeatherReply
        {
            Weather = "Sunny",
            Temperature = 25.ToString()
        };
        return Task.FromResult(reply);
    }
}
