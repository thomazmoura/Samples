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
        return Enumerable.Range(0, quantidade)
            .Select(indice =>
            {
                _seed++;
                var id = pularId ? 0 : _seed;
                var pessoa = new Bogus.Person("pt_BR");
                return new Pessoa()
                {
                    Id = id,
                    Nome = pessoa.FullName,
                    Ativo = pessoa.Random.Bool(0.8f),
                    DataDeNascimento = pessoa.DateOfBirth
                };
            });
    }

    public IEnumerable<Produto> GerarProdutos(int quantidade)
    {
        var faker = new Faker("pt_BR");
        return Enumerable.Range(0, quantidade)
            .Select(indice => new Produto()
            {
                ValorUnitario = faker.Random.Decimal(1.99m, 99.99m),
                Descricao = faker.Commerce.ProductName()
            });
    }

    public IEnumerable<Compra> GerarCompras(IEnumerable<Pessoa> pessoas, IEnumerable<Produto> produtos)
    {
        var faker = new Faker("pt_BR");
        var idsDeProdutos = produtos
            .Select(produto => produto.Id)
            .ToList();
        foreach (var pessoa in pessoas)
        {
            var quantidadeDeCompras = faker.Random.Int(1, 5);
            pessoa.Compras = Enumerable.Range(0, quantidadeDeCompras)
                .Select(indice => new Compra()
                {
                    PessoaId = pessoa.Id,
                    DataDaCompra = faker.Date.Past()
                }).ToList();
            foreach (var compra in pessoa.Compras)
            {
                var quantidadeDeItens = faker.Random.Int(1, 15);
                compra.ItensDaCompra = Enumerable.Range(0, quantidadeDeCompras)
                    .Select(indice => new ItemDaCompra()
                    {
                        ProdutoId = faker.PickRandom(idsDeProdutos),
                        Quantidade = faker.Random.Int(1, 10)
                    }).ToList();
            }
        }
        return pessoas.SelectMany(pessoa => pessoa.Compras);
    }

    public int ObterSeed() => _seed;
}

