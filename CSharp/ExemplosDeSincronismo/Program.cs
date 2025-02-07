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
    .AddScoped<IExemplosDeSincronismoServico, ExemploDeSincronismoUsandoEF>()
    .AddScoped<IGarantidorDeDados, GarantidorDeDados>()
    .AddScoped<IGeradorDePessoas, GeradorDePessoasComFaker>()
    .AddHostedService<Worker>();

var host = builder
    .Build();
host.Run();
