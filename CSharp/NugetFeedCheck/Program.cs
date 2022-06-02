
var uriOnArgs = args
    .Where(arg => arg.StartsWith("https://"));
var uri = uriOnArgs.Any() ?
    uriOnArgs.First() :
    "https://api.nuget.org/v3/index.json";

var client = new HttpClient();
Console.WriteLine($"URI to be called: {uri}");
var response = await client.GetAsync(uri);
string msg = $"If you see this, your machine has no TLS/SSL issues calling {uri}";
Console.WriteLine(msg);
