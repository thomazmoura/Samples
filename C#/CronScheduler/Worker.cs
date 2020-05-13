using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CronScheduler
{
    public class Worker : IHostedService
    {
        private Timer _timer;
        private Timer _cronTimer;
        private readonly ILogger<Worker> _logger;
        private readonly IOptionsMonitor<Configuracao> _configuracao;

        public Worker(ILogger<Worker> logger, IOptionsMonitor<Configuracao> configuracao)
        {
            _configuracao = configuracao;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var proximaExecucao = ObterHorarioDaProximaExecucao();
            var tempoParaAProximaExecucao = ObterTempoAteAProximaExecucao(proximaExecucao);
            _logger.LogInformation($"A execução está prevista para {proximaExecucao.ToLocalTime()} que é daqui a {tempoParaAProximaExecucao}");
            _timer = new Timer(_ => LogarTempo(), null, tempoParaAProximaExecucao, _configuracao.CurrentValue.Frequencia);
            Task.Run(() => LogarTempoComSleep(cancellationToken));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tchau! ;)");
        }

        private void LogarTempoComSleep(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Estou rodando o agendamento do {DateTime.Now}");
                var horarioDaProximaExecucao = ObterHorarioDaProximaExecucao();
                var tempoAteAProximaExecucao = horarioDaProximaExecucao - DateTime.UtcNow;
                Thread.Sleep(tempoAteAProximaExecucao);
            }
        }

        private DateTime ObterHorarioDaProximaExecucao()
        {
            var configuracao = _configuracao.CurrentValue;
            var cronExpression = CronExpression.Parse(configuracao.Agendamento, CronFormat.IncludeSeconds);
            return cronExpression.GetNextOccurrence(DateTime.UtcNow).Value;
        }

        private TimeSpan ObterTempoAteAProximaExecucao(DateTime horarioDaProximaExecucao)
        {
            return horarioDaProximaExecucao - DateTime.UtcNow;
        }

        private void LogarTempo()
        {
            _logger.LogInformation($"Estou rodando em {DateTime.Now}");
            Thread.Sleep(_configuracao.CurrentValue.FrequenciaDaSegundaMensagem);
            _logger.LogDebug($"Já rodei faz tempo. Agora está em {DateTime.Now}");
        }
    }
}
