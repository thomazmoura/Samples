namespace ExemplosDeIEnumerableEIQueryable.Servicos;

public class ExemplosDeConsultaComIQueryableServico : IExemplosDeConsultaServico
{
    private int _verificacaoDeEstarAtivo = 0;
    private int _verificacaoDeSerPar = 0;
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

    private bool EstaAtivo(Pessoa pessoa)
    {
        _verificacaoDeEstarAtivo++;
        return pessoa.Ativo;
    }

    private void DefinirPessoasComOMesmoAniversario(IEnumerable<Pessoa> pessoas)
    {
        var stopWatch = Stopwatch.StartNew();
        foreach (var pessoa in pessoas)
        {
            var aniversario = (pessoa.DataDeNascimento.Day, pessoa.DataDeNascimento.Month);
            // Mencionar comportamento na ausência de  do ToList
            pessoa.PessoasAtivasComMesmaDataDeAniversario = pessoas.Where(pessoa => aniversario == (pessoa.DataDeNascimento.Day, pessoa.DataDeNascimento.Month)).ToList();
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

    private bool IdSejaPar(Pessoa pessoa)
    {
        _verificacaoDeSerPar++;
        return pessoa.Id % 2 == 0;
    }
}

