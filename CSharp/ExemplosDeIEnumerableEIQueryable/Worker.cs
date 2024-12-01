using System.Diagnostics;
using System.Text.Json;
using Bogus;

namespace ExemplosDeIEnumerableEIQueryable;

public class Worker : BackgroundService
{
    private int _seed = 1;
    private int _verificacaoDeEstarAtivo = 0;
    private int _verificacaoDeSerPar = 0;
    private readonly ILogger<Worker> _logger;

    private readonly IServiceProvider _serviceProvider;
    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        Randomizer.Seed = new Random(42);
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var stopWatch = Stopwatch.StartNew();
        using var escopo = _serviceProvider.CreateScope();
        var contexto = escopo.ServiceProvider.GetRequiredService<Contexto>();
        _logger.LogInformation("Iniciando sistema");

        GarantirDadosDeExemplo(contexto);

        var pessoas = contexto.Pessoas;
        var pessoasComIdPar = pessoas
            .Where(IdSejaPar);
        var pessoasComIdImpar = pessoas
            .Where(pessoa => !IdSejaPar(pessoa));
        var pessoasAtivas = pessoas
            .Where(EstaAtivo);
        var quantidadePar = pessoasComIdPar.Count();
        var quantidadeImpar = pessoasComIdImpar.Count();

        DefinirPessoasComOMesmoAniversario(pessoas);

        _logger.LogInformation("\nO resultado é: \n Seed:{Seed}\n Verificação par: {EPar}\n\n", _seed, _verificacaoDeSerPar);
        _logger.LogInformation("\nQuantidade de pessoas ativas: {PessoasAtivas}", pessoasAtivas.Count());
        var pessoasAtivasCujoAniversarioECompartilhado = pessoas.Where(pessoa => pessoa.PessoasAtivasComMesmaDataDeAniversario.Count() > 1);
        _logger.LogInformation("\nQuantidade de pessoas ativas que compartilham aniversário: {PessoasAtivas}", pessoasAtivasCujoAniversarioECompartilhado.Count());
        stopWatch.Stop();
        _logger.LogInformation("\nA execução demorou:\n {Duracao}\n\n", stopWatch.Elapsed);

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            var datasDaPessoasQueNaoCompartilhamAniversario = pessoas
                .Except(pessoasAtivasCujoAniversarioECompartilhado)
                .Select(pessoa => pessoa.DataDeNascimento.ToString("dd/MM"))
                .Distinct();
            _logger.LogDebug("Datas de aniversário não compartilhadas: {Datas}", string.Join(", ", datasDaPessoasQueNaoCompartilhamAniversario));
        }
    }

    private IEnumerable<Pessoa> GerarPessoas()
    {
        var faker = new Faker("pt_BR");

        return Enumerable.Range(0, 150000)
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

    private void GarantirDadosDeExemplo(Contexto contexto)
    {
        if (contexto.Pessoas.Any())
            return;

        var pessoas = GerarPessoas();
        contexto.AddRange(pessoas);
        contexto.SaveChangesAsync();

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
        foreach (var pessoa in pessoas)
        {
            var aniversario = (pessoa.DataDeNascimento.Day, pessoa.DataDeNascimento.Month);
            // Mencionar comportamento na ausência de  do ToList
            pessoa.PessoasAtivasComMesmaDataDeAniversario = pessoas.Where(pessoa => aniversario == (pessoa.DataDeNascimento.Day, pessoa.DataDeNascimento.Month)).ToList();
        }
        stopWatch.Stop();
        _logger.LogInformation("\nA definição de pessoas com o mesmo aniversário demorou:\n {Duracao}\n\n", stopWatch.Elapsed);
    }
}
