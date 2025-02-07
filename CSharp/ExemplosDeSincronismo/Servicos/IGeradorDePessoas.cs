namespace ExemplosDeSincronismo.Servicos;

public interface IGeradorDePessoas
{
    IEnumerable<Pessoa> GerarPessoas(int quantidade, bool pularId = false);
    IEnumerable<Produto> GerarProdutos(int quantidade);
    IEnumerable<Compra> GerarCompras(IEnumerable<Pessoa> pessoas, IEnumerable<Produto> produtos);
    int ObterSeed();
}
