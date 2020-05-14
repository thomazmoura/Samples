using System;
using System.Collections.Generic;
using System.Linq;
using Cronos;

namespace CronDisplay
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!args.Any())
            {
                Console.WriteLine("You need to inform the Cron Schedule");
                return;
            };
            var cronSchedule = args[0];

            var cronExpression = CronExpression.Parse(cronSchedule, CronFormat.IncludeSeconds);
            var next20OcurrencesThisYear = cronExpression
                .GetOccurrences(DateTime.UtcNow, DateTime.UtcNow.AddYears(1))
                .Take(20);

            foreach(var ocurrence in next20OcurrencesThisYear)
            {
                if(ocurrence == next20OcurrencesThisYear.First())
                    Console.WriteLine($"First it will fire on {ocurrence}, which is on {ocurrence.ToLocalTime()} on local time");
                else
                    Console.WriteLine($"Then it will fire on {ocurrence}, which is on {ocurrence.ToLocalTime()} on local time");
            }
        }
    }
}
