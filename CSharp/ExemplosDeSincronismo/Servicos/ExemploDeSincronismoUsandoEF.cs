
namespace ExemplosDeSincronismo.Servicos;

public class ExemploDeSincronismoUsandoEF : IExemplosDeSincronismoServico
{
    private readonly Contexto _contexto;
    private readonly ILogger _logger;
    public ExemploDeSincronismoUsandoEF(Contexto contexto, ILogger<ExemploDeSincronismoUsandoRowVersion> logger)
    {
        _contexto = contexto;
        _logger = logger;
    }

    public async Task ExecutarAsync(CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var produtos = await _contexto.Produtos
            .ToListAsync(cancellationToken);
        var produtosPersisitidos = await _contexto.ProdutosStage1
            .ToListAsync(cancellationToken);
        var idPorIdOriginal = produtosPersisitidos
            .ToDictionary(produto => produto.OriginalId, produto => produto.Id);
        _contexto.RemoveRange(produtosPersisitidos);

        var produtosAtualizados = produtos
            .Select(produto => new ProdutoStage1
            {
                OriginalId = produto.Id,
                Descricao = produto.Descricao,
                ValorUnitario = produto.ValorUnitario
            })
            .ToList();

        foreach (var produto in produtosAtualizados)
        {
            if (idPorIdOriginal.TryGetValue(produto.OriginalId, out var id))
            {
                produto.Id = id;
            }
        }
        _contexto.AddRange(produtosAtualizados);
        await _contexto.SaveChangesAsync(cancellationToken);
        stopwatch.Stop();
        _logger.LogInformation("Tempo para sincronizar produtos: {Tempo}", stopwatch.Elapsed);
    }
}
