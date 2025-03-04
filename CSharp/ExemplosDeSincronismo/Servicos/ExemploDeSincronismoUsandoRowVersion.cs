﻿
namespace ExemplosDeSincronismo.Servicos;

public class ExemploDeSincronismoUsandoRowVersion : IExemplosDeSincronismoServico
{
    private readonly Contexto _contexto;
    private readonly ILogger _logger;
    public ExemploDeSincronismoUsandoRowVersion(Contexto contexto, ILogger<ExemploDeSincronismoUsandoRowVersion> logger)
    {
        _contexto = contexto;
        _logger = logger;
    }

    public async Task ExecutarAsync(CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var versaoPorIdOriginais = (await _contexto.Pessoas
            .Select(pessoa => new { pessoa.Id, pessoa.Version })
            .ToListAsync(cancellationToken))
            .ToDictionary(pessoa => pessoa.Id, pessoa => pessoa.Version);
        var versaoPorOriginalIdPersistidas = (await _contexto.PessoasStage1
            .Select(pessoa => new { pessoa.OriginalId, pessoa.Version })
            .ToListAsync(cancellationToken))
            .ToDictionary(pessoa => pessoa.OriginalId, pessoa => pessoa.Version);
        var idPorOriginalId = (await _contexto.PessoasStage1
            .Select(pessoa => new { pessoa.OriginalId, pessoa.Id })
            .ToListAsync(cancellationToken))
            .ToDictionary(pessoa => pessoa.OriginalId, pessoa => pessoa.Id);
        stopwatch.Stop();
        _logger.LogInformation("Tempo para obter chaves: {Tempo}", stopwatch.Elapsed);

        stopwatch.Restart();
        var chavesDePessoasParaInserir = versaoPorIdOriginais.Keys.Except(versaoPorOriginalIdPersistidas.Keys);
        var chavesDePessoasParaAtualizar = versaoPorIdOriginais
            .Where(chave => versaoPorOriginalIdPersistidas.TryGetValue(chave.Key, out var versao) &&
                !chave.Value.SequenceEqual(versao))
            .Select(chave => chave.Key);
        var chavesDePessoasParaExcluir = versaoPorOriginalIdPersistidas.Keys.Except(versaoPorIdOriginais.Keys);
        stopwatch.Stop();
        _logger.LogInformation("Tempo para obter verificar chaves: {Tempo}", stopwatch.Elapsed);

        stopwatch.Restart();
        var pessoasParaInserir = await _contexto.Pessoas
            .Where(pessoa => chavesDePessoasParaInserir.Contains(pessoa.Id))
            .Select(pessoa => new PessoaStage1
            {
                OriginalId = pessoa.Id,
                Nome = pessoa.Nome,
                Apelido = pessoa.Apelido,
                Ativo = pessoa.Ativo,
                DataDeNascimento = pessoa.DataDeNascimento,
                Version = pessoa.Version
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        _contexto.PessoasStage1.AddRange(pessoasParaInserir);
        stopwatch.Stop();
        _logger.LogInformation("Tempo para preparar dados novos para inserção: {Tempo}. Quantidade de inserções: {Quantidade}", stopwatch.Elapsed, pessoasParaInserir.Count);

        stopwatch.Restart();
        var pessoasParaAtualizar = await _contexto.Pessoas
            .Where(pessoa => chavesDePessoasParaAtualizar.Contains(pessoa.Id))
            .Select(pessoa => new PessoaStage1
            {
                Id = idPorOriginalId[pessoa.Id],
                OriginalId = pessoa.Id,
                Nome = pessoa.Nome,
                Apelido = pessoa.Apelido,
                Ativo = pessoa.Ativo,
                DataDeNascimento = pessoa.DataDeNascimento,
                Version = pessoa.Version
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        _contexto.PessoasStage1.UpdateRange(pessoasParaAtualizar);
        stopwatch.Stop();
        _logger.LogInformation("Tempo para preparar dados de atualização: {Tempo}. Quantidade de atualizações: {Quantidade}", stopwatch.Elapsed, pessoasParaAtualizar.Count);

        stopwatch.Restart();
        await _contexto.SaveChangesAsync(cancellationToken);
        stopwatch.Stop();
        _logger.LogInformation("Tempo para efetivar inserções e atualizações: {Tempo}. Inserções: {Insercoes}. Atualizações {Atualizacoes}", stopwatch.Elapsed, pessoasParaInserir.Count, pessoasParaAtualizar.Count);

        stopwatch.Restart();
        var pessoasParaExcluir = await _contexto.PessoasStage1
            .Where(pessoa => chavesDePessoasParaExcluir.Contains(pessoa.OriginalId))
            .ExecuteDeleteAsync(cancellationToken);
        stopwatch.Stop();
        _logger.LogInformation("Tempo para excluir dados removidos: {Tempo}. Quantidade de deleções: {Quantidade}", stopwatch.Elapsed, chavesDePessoasParaExcluir.Count());
    }
}
