namespace TestContainerSample.Console.Servicos;

public class ProdutoServico
{
    private readonly Contexto _contexto;
    public ProdutoServico(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task AcrescentarProdutoAsync(Produto produto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(produto.Descricao))
            throw new ArgumentException("Descrição do produto é obrigatória");

        _contexto.Produtos.Add(produto);
        await _contexto.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarPrecoAsync(int produtoId, decimal preco, CancellationToken cancellationToken = default)
    {
        if (preco <= 0)
            throw new ArgumentException("O preço do produto deve ser maior que zero");

        var produto = _contexto.Produtos.SingleOrDefault(p => p.Id == produtoId);

        if (produto is null)
            throw new ArgumentException("Não foi possível identificar o produto");

        produto.Preco = preco;
        await _contexto.SaveChangesAsync(cancellationToken);
    }
}
