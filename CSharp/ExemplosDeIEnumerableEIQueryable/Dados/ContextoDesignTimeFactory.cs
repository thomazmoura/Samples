namespace ExemplosDeIEnumerableEIQueryable.Dados;

public class ContextoDesignTimeFactory : IDesignTimeDbContextFactory<Contexto>
{
    private IConfiguration _configuration { get; set; }

    public ContextoDesignTimeFactory()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .Build();
    }

    public Contexto CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Contexto>();
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString(nameof(Contexto)));
        //optionsBuilder.UseNpgsql(_configuration.GetConnectionString(nameof(ContextoDeExemplo)));

        return new Contexto(optionsBuilder.Options);
    }
}
