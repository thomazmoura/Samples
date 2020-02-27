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
                Console.WriteLine("Digite o valor a ser verificado (q para sair)");
                input = Console.ReadLine();
                var flags = (RuleLevel)byte.Parse(input);
                Console.WriteLine($"O valor digitado corresponde a: {flags}");

                foreach(var flag in Enum.GetValues(typeof(RuleLevel)).Cast<RuleLevel>())
                {
                    if((flags & flag) > 0)
                        Console.WriteLine($"Possui acesso ao nível {flag}");
                }

                if((flags & (RuleLevel.Level1 | RuleLevel.Level2 | RuleLevel.Level3)) > 0)
                    Console.WriteLine("Possui acesso a um dos três primeiros níveis");
            } while (input != "q" && input != "exit");
        }
    }

    [Flags]
    public enum RuleLevel: byte
    {
        Level1 = 1,
        Level2 = 2,
        Level3 = 4,
        Level4 = 8,
        Level5 = 16,
        Level6 = 32
    }
}
