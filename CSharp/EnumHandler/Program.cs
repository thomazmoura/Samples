using System;
using System.Linq;

namespace EnumHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            do
            {
                Console.WriteLine("Type the byte you want to check (q to exit):");
                input = Console.ReadLine();
                var inputAsFlags = (RuleLevel)byte.Parse(input);

                Console.WriteLine($"The input is equals to: {inputAsFlags}");

                foreach (var ruleLevelFlag in Enum.GetValues(typeof(RuleLevel)).Cast<RuleLevel>())
                {
                    if (inputAsFlags.HasFlag(ruleLevelFlag))
                        Console.WriteLine($"Has access to {ruleLevelFlag}");
                }

                if (inputAsFlags.HasFlag(RuleLevel.Level1 | RuleLevel.Level2 | RuleLevel.Level3))
                    Console.WriteLine("Has Access to any of the first three levels");

                if(!inputAsFlags.HasFlag(RuleLevel.Level4 | RuleLevel.Level5 | RuleLevel.Level6))
                    Console.WriteLine("The user doesn't have access to all the last three levels");

            } while (input != "q" && input != "exit");
        }
    }

    [Flags]
    public enum RuleLevel : byte
    {
        NoAcess = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 4,
        Level4 = 8,
        Level5 = 16,
        Level6 = 32
    }
}
