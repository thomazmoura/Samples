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
        private readonly ILogger<Worker> _logger;
        private readonly IOptionsMonitor<Configuracao> _configuracao;

        public Worker(ILogger<Worker> logger, IOptionsMonitor<Configuracao> configuracao)
        {
            _configuracao = configuracao;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var configuracao = _configuracao.CurrentValue;
            var cronExpression = CronExpression.Parse(configuracao.Agendamento, CronFormat.IncludeSeconds);
            var proximaExecucao = cronExpression.GetNextOccurrence(DateTime.UtcNow).Value;
            var tempoParaAProximaExecucao = (proximaExecucao - DateTime.UtcNow);
            _logger.LogInformation($"A execução está prevista para {proximaExecucao.ToLocalTime()} que é daqui a {tempoParaAProximaExecucao}");
            _timer = new Timer(_ => LogarTempo(), null, tempoParaAProximaExecucao, configuracao.Frequencia);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tchau! ;)");
        }

        private void LogarTempo()
        {
            _logger.LogInformation($"Estou rodando em {DateTime.Now}");
            Thread.Sleep(_configuracao.CurrentValue.FrequenciaDaSegundaMensagem);
            _logger.LogDebug($"Já rodei faz tempo. Agora está em {DateTime.Now}");
        }
    }
}
