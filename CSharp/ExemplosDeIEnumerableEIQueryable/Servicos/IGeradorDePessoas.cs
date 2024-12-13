namespace ExemplosDeIEnumerableEIQueryable.Servicos;

public interface IGeradorDePessoas
{
    IEnumerable<Pessoa> GerarPessoas(int quantidade, bool pularId = false);
    int ObterSeed();
}
