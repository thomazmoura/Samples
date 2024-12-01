using ExemplosDeIEnumerableEIQueryable;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddDbContext<Contexto>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(Contexto))))
    .AddHostedService<Worker>();

var host = builder
    .Build();
host.Run();
