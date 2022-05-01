using Cronos;

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
var firstOcurrence = next20OcurrencesThisYear.First();

Console.WriteLine($"First it will fire on {firstOcurrence}, which is on {firstOcurrence.ToLocalTime()} on local time");
foreach(var ocurrence in next20OcurrencesThisYear.Skip(1))
    Console.WriteLine($"Then it will fire on {ocurrence}, which is on {ocurrence.ToLocalTime()} on local time");
