namespace ExemplosDeIEnumerableEIQueryable.Servicos;

public class ExemplosDeConsultaComIQueryableServico : IExemplosDeConsultaServico
{
    private readonly IGeradorDePessoas _geradorDePessoas;
    private readonly ILogger<ExemplosDeConsultaComIQueryableServico> _logger;
    private readonly Contexto _contexto;
    public ExemplosDeConsultaComIQueryableServico(
        IGeradorDePessoas geradorDePessoas,
        ILogger<ExemplosDeConsultaComIQueryableServico> logger,
        Contexto contexto
    )
    {
        _geradorDePessoas = geradorDePessoas;
        _logger = logger;
        _contexto = contexto;
    }

    public async Task ExecutarAsync(CancellationToken cancellationToken)
    {

        var stopWatchGeral = Stopwatch.StartNew();
        _logger.LogInformation("Iniciando sistema");

        var pessoas = ObterPessoas()
            .Where(pessoa => !string.IsNullOrWhiteSpace(pessoa.Nome))
            .Take(10)
            .ToList();

        var nomes = string.Join(", ", pessoas);
        Console.WriteLine(nomes);

        stopWatchGeral.Stop();
        _logger.LogInformation("\nA execução demorou:\n {Duracao}\n\n", stopWatchGeral.Elapsed);
    }

    private bool EstaAtivo(Pessoa pessoa)
    {
        return pessoa.Ativo;
    }

    private void DefinirPessoasComOMesmoAniversario(IEnumerable<Pessoa> pessoas)
    {
        var stopWatch = Stopwatch.StartNew();
        var pessoasAgrupadasPorAniversario = pessoas
            .GroupBy(pessoa => (pessoa.DataDeNascimento.Day, pessoa.DataDeNascimento.Month), pessoa => pessoa)
            .ToDictionary(grupo => grupo.Key);
        foreach (var pessoa in pessoas)
        {
            var aniversario = (pessoa.DataDeNascimento.Day, pessoa.DataDeNascimento.Month);
            pessoa.PessoasAtivasComMesmaDataDeAniversario = pessoasAgrupadasPorAniversario[aniversario];
        }
        stopWatch.Stop();
        _logger.LogInformation("\nA definição de pessoas com o mesmo aniversário demorou:\n {Duracao}\n\n", stopWatch.Elapsed);
    }

    private IQueryable<Pessoa> ObterPessoas()
    {
        if (!_contexto.Pessoas.Any())
        {
            var pessoas = _geradorDePessoas.GerarPessoas(15000, pularId: true);
            _contexto.Pessoas.AddRange(pessoas);
            _contexto.SaveChanges();
        }
        return _contexto.Pessoas;
    }
}

