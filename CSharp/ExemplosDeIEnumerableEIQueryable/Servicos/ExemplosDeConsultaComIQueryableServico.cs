namespace ExemplosDeIEnumerableEIQueryable.Servicos;

public class ExemplosDeConsultaComIQueryableServico : IExemplosDeConsultaServico
{
    private readonly IGeradorDePessoas _geradorDePessoas;
    private readonly ILogger<ExemplosDeConsultaComIQueryableServico> _logger;
    private readonly Contexto _contexto;
    public ExemplosDeConsultaComIQueryableServico(
        IGeradorDePessoas geradorDePessoas,
        ILogger<ExemplosDeConsultaComIQueryableServico> logger,
        Contexto contexto
    )
    {
        _geradorDePessoas = geradorDePessoas;
        _logger = logger;
        _contexto = contexto;
    }

    public async Task ExecutarAsync(CancellationToken cancellationToken)
    {
        await GarantirDadosAsync(cancellationToken);
        var stopWatchGeral = Stopwatch.StartNew();
        _logger.LogInformation("Iniciando sistema");

        var compras = await _contexto.Compras
            .OrderBy(compra => compra.Id)
            .Take(50)
            .ToListAsync(cancellationToken);
        //.Where(compra => compra.ItensDaCompra.Any(item => item.ValorUnitario > 50))
        //.CountAsync(cancellationToken);
        _logger.LogInformation("\n ->> Quantidade de compras com valor de mais de 50: ({Quantidade})", compras.Count);

        //var pessoasQueGastaramMaisDe500reais = ObterPessoas()
        //.Select(pessoa => new
        //{
        //NomeDaPessoa = pessoa.Nome,
        //Gasto = pessoa.Compras.SelectMany(compra => compra.ItensDaCompra.Select(item => item.Quantidade * item.ValorUnitario)).Sum()
        //})
        //.Where(pessoa => pessoa.Gasto > 5000)
        //.ToList();
        //var resultado = string.Join("\n", pessoasQueGastaramMaisDe500reais.Select(pessoa => $"{pessoa.NomeDaPessoa} - {pessoa.Gasto}"));
        //_logger.LogInformation("\n ->> Pessoas que gastaram mais de 500 reais ({Quantidade}):\n{Resultado}", pessoasQueGastaramMaisDe500reais.Count, resultado);

        stopWatchGeral.Stop();
        _logger.LogInformation("\nA execução demorou:\n {Duracao}\n\n", stopWatchGeral.Elapsed);
    }

    private IQueryable<Pessoa> ObterPessoas()
    {
        return _contexto.Pessoas;
    }

    private async Task GarantirDadosAsync(CancellationToken cancellationToken)
    {
        var stopWatchCriacao = Stopwatch.StartNew();
        if (!_contexto.Pessoas.Any())
        {
            var pessoas = _geradorDePessoas.GerarPessoas(150000, pularId: true);
            _contexto.Pessoas.AddRange(pessoas);
            await _contexto.SaveChangesAsync(cancellationToken);
        }
        if (!_contexto.Produtos.Any())
        {
            var produtos = _geradorDePessoas.GerarProdutos(5000);
            _contexto.Produtos.AddRange(produtos);
            await _contexto.SaveChangesAsync(cancellationToken);
        }
        if (!_contexto.Compras.Any())
        {
            var compras = _geradorDePessoas.GerarCompras(_contexto.Pessoas.ToList(), _contexto.Produtos.ToList());
            _contexto.Compras.AddRange(compras);
            await _contexto.SaveChangesAsync(cancellationToken);
        }
        stopWatchCriacao.Stop();
        _logger.LogInformation("A criação de dados no banco demorou: {Duracao}", stopWatchCriacao.Elapsed);
    }
}

//var quantidadeDePessoasQueCompraramMaisDe2Vezes = ObterPessoas()
//.Where(pessoa => pessoa.Compras.Count() > 2)
//.Count();
//_logger.LogInformation(
//"\n ->> Quantidade de pessoas que compraram mais de 2 vezes: {Quantidade}",
//quantidadeDePessoasQueCompraramMaisDe2Vezes
//);

//var quantidadeDeMacedos = ObterPessoas()
//.Where(pessoa => pessoa.Nome.EndsWith("Macedo"))
//.Count();
//_logger.LogInformation(
//"\n ->> Quantidade de pessoas que têm o sobrenome Macedo: {Quantidade}",
//quantidadeDeMacedos
//);

//var pessoasQueGastaramMaisDe500reais = ObterPessoas()
//.Select(pessoa => new
//{
//NomeDaPessoa = pessoa.Nome,
//Gasto = pessoa.Compras.SelectMany(compra => compra.ItensDaCompra.Select(item => item.Quantidade * item.Produto.ValorUnitario)).Sum()
//})
//.Where(pessoa => pessoa.Gasto > 5000)
//.ToList();
//var resultado = string.Join("\n", pessoasQueGastaramMaisDe500reais.Select(pessoa => $"{pessoa.NomeDaPessoa} - {pessoa.Gasto}"));
//_logger.LogInformation("\n ->> Pessoas que gastaram mais de 500 reais ({Quantidade}):\n{Resultado}", pessoasQueGastaramMaisDe500reais.Count, resultado);

