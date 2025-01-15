var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddDbContext<Contexto>(
        options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(Contexto)))
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
    )
    .AddScoped<IExemplosDeConsultaServico, ExemplosDeConsultaComIQueryableServico>()
    .AddScoped<IGeradorDePessoas, GeradorDePessoasComFaker>()
    .AddHostedService<Worker>();

var host = builder
    .Build();
host.Run();
