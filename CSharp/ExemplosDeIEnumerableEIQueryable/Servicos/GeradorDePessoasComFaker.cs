namespace ExemplosDeIEnumerableEIQueryable.Servicos;

public class GeradorDePessoasComFaker : IGeradorDePessoas
{
    private int _seed = 1;

    public GeradorDePessoasComFaker()
    {
        Randomizer.Seed = new Random(42);
    }

    public IEnumerable<Pessoa> GerarPessoas(int quantidade, bool pularId = false)
    {
        var faker = new Faker("pt_BR");

        return Enumerable.Range(0, quantidade)
            .Select(indice =>
            {
                _seed++;
                var id = pularId ? 0 : _seed;
                var pessoa = new Person();
                return new Pessoa()
                {
                    Id = id,
                    Nome = pessoa.FullName,
                    Ativo = pessoa.Random.Bool(0.8f),
                    DataDeNascimento = pessoa.DateOfBirth
                };
            });
    }

    public int ObterSeed() => _seed;
}

