namespace ExemplosDeIEnumerableEIQueryable.Servicos;

public interface IGeradorDePessoas
{
    IEnumerable<Pessoa> GerarPessoas(int quantidade);
    int ObterSeed();
}
