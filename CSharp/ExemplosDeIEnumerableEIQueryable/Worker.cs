namespace ExemplosDeIEnumerableEIQueryable;

public class Worker : BackgroundService
{
    private int s_seed = 1;
    private int s_execucoes = 0;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        List<Pessoa> pessoas = [
            new Pessoa() {
                Nome = "Teste 1",
                Ativo = false
            },
            new Pessoa() {
                Nome = "Teste 2",
                Ativo = true
            },
            new Pessoa() {
                Nome = "Teste 3",
                Ativo = true
            },
           new Pessoa() {
                Nome = "Teste 4",
                Ativo = true
            },
        ];

        var consulta = pessoas
            .Where(EstaAtivo)
            .Select(DefinirValorDoId)
            .Where(VerificarValor);
        var temQualquerValor = consulta.Where(valor => true);
        var quantidade = consulta.Count();


        Console.WriteLine();
        Console.WriteLine("O resultado é:");
        Console.WriteLine(s_seed);
        Console.WriteLine(s_execucoes);
        Console.WriteLine(temQualquerValor);
        Console.WriteLine(temQualquerValor);
        Console.WriteLine();
    }

    private bool VerificarValor(Pessoa pessoa)
    {
        return pessoa.Id % 2 == 0;
    }

    private Pessoa DefinirValorDoId(Pessoa pessoa)
    {
        pessoa.Id = s_seed++;
        return pessoa;
    }

    private bool EstaAtivo(Pessoa pessoa)
    {
        s_execucoes++;
        return pessoa.Ativo;
    }
}
