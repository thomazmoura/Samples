public class SystemConfig
{
    public string APIUrl { get; init; }
    public string AuthorityURL { get; init; }
    public string ClientName { get; init; }
    public string ClientSecret { get; init; }

    public SystemConfig(string[] args)
    {
        if(args.Length < 4)
            throw new ArgumentException("Not enough args passed to SystemConfig.");
        APIUrl = args[0];
        AuthorityURL = args[1];
        ClientName = args[2];
        ClientSecret = args[3];
    }
}
