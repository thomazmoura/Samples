namespace ExemplosDeIEnumerableEIQueryable.Servicos;

public class GeradorDePessoasComFaker : IGeradorDePessoas
{
    private int _seed = 1;

    public GeradorDePessoasComFaker()
    {
        Randomizer.Seed = new Random(42);
    }

    public IEnumerable<Pessoa> GerarPessoas(int quantidade)
    {
        var faker = new Faker("pt_BR");

        return Enumerable.Range(0, quantidade)
            .Select(indice =>
            {
                var pessoa = new Person();
                return new Pessoa()
                {
                    Id = _seed++,
                    Nome = pessoa.FullName,
                    Ativo = pessoa.Random.Bool(0.8f),
                    DataDeNascimento = pessoa.DateOfBirth
                };
            });
    }

    public int ObterSeed() => _seed;
}

