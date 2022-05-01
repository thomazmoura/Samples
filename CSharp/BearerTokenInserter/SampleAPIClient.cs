public interface ISampleAPIClient
{
    Task<object?> Get(string url);
}

public class SampleAPIClient : ISampleAPIClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SampleAPIClient> _logger;
    public SampleAPIClient(HttpClient httpClient, ILogger<SampleAPIClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<object?> Get(string url)
    {
        var result = await _httpClient.GetAsync(url);
        if(result.IsSuccessStatusCode is false)
        {
            var conteudo = await result.Content.ReadAsStringAsync();
            _logger.LogWarning($"Call failed. Status Code: {result.StatusCode}. Content: {conteudo}");
            return null;
        }
        return await result.Content.ReadFromJsonAsync<object>();
    }
}
