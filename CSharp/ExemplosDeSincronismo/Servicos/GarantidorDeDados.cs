namespace ExemplosDeSincronismo.Servicos;

public class GarantidorDeDados : IGarantidorDeDados
{
    private readonly Contexto _contexto;
    private readonly IGeradorDePessoas _geradorDeDados;
    public GarantidorDeDados(Contexto contexto, IGeradorDePessoas geradorDeDados)
    {
        _contexto = contexto;
        _geradorDeDados = geradorDeDados;
    }

    public async Task GarantirQueHaDadosNaBase(CancellationToken cancellationToken)
    {
        var pessoas = new List<Pessoa>();
        var produtos = new List<Produto>();
        if (!_contexto.Pessoas.Any())
        {
            pessoas = _geradorDeDados.GerarPessoas(1500).ToList();
            _contexto.Pessoas.AddRange(pessoas);
        }
        if (!_contexto.Produtos.Any())
        {
            produtos = _geradorDeDados.GerarProdutos(1500).ToList();
            _contexto.Produtos.AddRange(produtos);
        }
        if (!_contexto.Compras.Any())
        {
            var compras = _geradorDeDados.GerarCompras(pessoas, produtos).ToList();
            _contexto.Compras.AddRange(compras);
        }
        await _contexto.SaveChangesAsync(cancellationToken);
    }
}
