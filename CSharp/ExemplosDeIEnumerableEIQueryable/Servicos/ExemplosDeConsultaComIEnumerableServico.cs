namespace ExemplosDeIEnumerableEIQueryable.Servicos;

public class ExemplosDeConsultaComIEnumerableServico : IExemplosDeConsultaServico
{
    private int _verificacaoDeEstarAtivo = 0;
    private int _verificacaoDeSerPar = 0;
    private readonly IGeradorDePessoas _geradorDePessoas;
    private readonly ILogger<ExemplosDeConsultaComIEnumerableServico> _logger;
    public ExemplosDeConsultaComIEnumerableServico(
        IGeradorDePessoas geradorDePessoas,
        ILogger<ExemplosDeConsultaComIEnumerableServico> logger
    )
    {
        _geradorDePessoas = geradorDePessoas;
        _logger = logger;
    }

    public async Task ExecutarAsync(CancellationToken cancellationToken)
    {

        var stopWatchGeral = Stopwatch.StartNew();
        _logger.LogInformation("Iniciando sistema");

        var pessoas = _geradorDePessoas.GerarPessoas(150000)
            .ToList();

        var pessoasComIdPar = pessoas
            .Where(IdSejaPar);
        var pessoasComIdImpar = pessoas
            .Where(pessoa => !IdSejaPar(pessoa));

        var pessoasAtivas = pessoas
            .Where(EstaAtivo);
        var quantidadePar = pessoasComIdPar.Count();
        var quantidadeImpar = pessoasComIdImpar.Count();

        DefinirPessoasComOMesmoAniversario(pessoas);

        _logger.LogInformation("\nO resultado é: \n Seed:{Seed}\n Verificação par: {EPar}\n\n", _geradorDePessoas.ObterSeed(), _verificacaoDeSerPar);
        _logger.LogInformation("\nQuantidade de pessoas ativas: {PessoasAtivas}", pessoasAtivas.Count());
        var pessoasAtivasCujoAniversarioECompartilhado = pessoas.Where(pessoa => pessoa.PessoasAtivasComMesmaDataDeAniversario.Count() > 1);
        _logger.LogInformation("\nQuantidade de pessoas ativas que compartilham aniversário: {PessoasAtivas}", pessoasAtivasCujoAniversarioECompartilhado.Count());

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            var datasDaPessoasQueNaoCompartilhamAniversario = pessoas
                .Except(pessoasAtivasCujoAniversarioECompartilhado)
                .Select(pessoa => pessoa.DataDeNascimento.ToString("dd/MM"))
                .Distinct();
            _logger.LogDebug("Datas de aniversário não compartilhadas: {Datas}", string.Join(", ", datasDaPessoasQueNaoCompartilhamAniversario));
        }
        stopWatchGeral.Stop();
        _logger.LogInformation("\nA execução demorou:\n {Duracao}\n\n", stopWatchGeral.Elapsed);
    }

    private bool IdSejaPar(Pessoa pessoa)
    {
        _verificacaoDeSerPar++;
        return pessoa.Id % 2 == 0;
    }

    private bool EstaAtivo(Pessoa pessoa)
    {
        _verificacaoDeEstarAtivo++;
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
}

