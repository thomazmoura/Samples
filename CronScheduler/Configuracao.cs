using System;
using Cronos;

namespace CronScheduler
{
    public class Configuracao
    {
        public string Agendamento { get; set; }
        public TimeSpan Frequencia { get; set; }
        public TimeSpan FrequenciaDaSegundaMensagem { get; set; }
    }
}