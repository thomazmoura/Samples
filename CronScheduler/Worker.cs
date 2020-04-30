using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CronScheduler
{
    public class Worker : IHostedService
    {
        private Timer _timer;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var cronExpression = CronExpression.Parse("0 0 17 * * *", CronFormat.IncludeSeconds);
            var proximaExecucao = cronExpression.GetNextOccurrence(DateTime.UtcNow).Value;
            var tempoParaAProximaExecucao = (proximaExecucao - DateTime.UtcNow);
            _logger.LogInformation($"A execução está prevista para {proximaExecucao.ToLocalTime()} que é daqui a {tempoParaAProximaExecucao}");
            _timer = new Timer(_ => LogarTempo(), null, tempoParaAProximaExecucao, TimeSpan.FromDays(1));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void LogarTempo()
        {
            _logger.LogInformation($"Estou rodando em {DateTime.Now}");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            _logger.LogDebug("Já rodei faz tempo");
        }
    }
}
