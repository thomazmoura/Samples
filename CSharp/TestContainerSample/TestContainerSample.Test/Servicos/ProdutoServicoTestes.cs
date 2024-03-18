namespace TestContainerSample.Test.Servicos;

public class ProdutoServicoTestes : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
        .Build();

    [Fact]
    public async Task AcrescentarProdutoAsync_QuandoADescricaoÉVálida_DevePersistirOProduto()
    {
        var connectionString = _msSqlContainer.GetConnectionString();
        var options = new DbContextOptionsBuilder<Contexto>()
            .UseSqlServer(connectionString)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;
        var contexto = new Contexto(options);
        contexto.Database.EnsureCreated();
        var servico = new ProdutoServico(contexto);
        var produtoEsperado = new Produto { Descricao = "Produto 1", Preco = 10.0m };

        await servico.AcrescentarProdutoAsync(produtoEsperado);

        var produtoObtido = await contexto.Produtos.SingleAsync();
        produtoObtido.Should().BeEquivalentTo(produtoEsperado, options => options.Excluding(p => p.Id));
    }

    [Fact]
    public async Task AcrescentarProdutoAsync_QuandoADescricaoNãoÉVálida_DeveLançarExceção()
    {
        var connectionString = _msSqlContainer.GetConnectionString();
        var options = new DbContextOptionsBuilder<Contexto>()
            .UseSqlServer(connectionString)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;
        var contexto = new Contexto(options);
        contexto.Database.EnsureCreated();
        var servico = new ProdutoServico(contexto);
        var produtoEsperado = new Produto { Descricao = "", Preco = 10.0m };

        Func<Task> executar = async () => await servico.AcrescentarProdutoAsync(produtoEsperado);

        await executar.Should().ThrowAsync<ArgumentException>().WithMessage("Descrição do produto é obrigatória");
    }

    [Fact]
    public async Task AcrescentarProdutoAsync_QuandoADescricaoNãoÉVálida_NãoDevePersistir()
    {
        var connectionString = _msSqlContainer.GetConnectionString();
        var options = new DbContextOptionsBuilder<Contexto>()
            .UseSqlServer(connectionString)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;
        var contexto = new Contexto(options);
        contexto.Database.EnsureCreated();
        var servico = new ProdutoServico(contexto);
        var produtoEsperado = new Produto { Descricao = "", Preco = 10.0m };

        try
        {
            await servico.AcrescentarProdutoAsync(produtoEsperado);
        }
        catch {}

        (await contexto.Produtos.SingleOrDefaultAsync()).Should().BeNull();
    }

    public Task InitializeAsync()
        => _msSqlContainer.StartAsync();

    public Task DisposeAsync()
        => _msSqlContainer.DisposeAsync().AsTask();
}
