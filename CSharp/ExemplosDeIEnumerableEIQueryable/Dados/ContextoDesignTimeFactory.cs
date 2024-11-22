namespace ExemplosDeIEnumerableEIQueryable.Dados;

public class ContextoDesignTimeFactory : IDesignTimeDbContextFactory<ContextoDeExemplo>
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

    public ContextoDeExemplo CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContextoDeExemplo>();
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString(nameof(ContextoDeExemplo)));
        //optionsBuilder.UseNpgsql(_configuration.GetConnectionString(nameof(ContextoDeExemplo)));

        return new ContextoDeExemplo(optionsBuilder.Options);
    }
}
