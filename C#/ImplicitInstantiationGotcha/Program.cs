using System;

namespace ImplicitInstantiationGotcha
{
    class Program
    {
        static void Main(string[] args)
        {
            var inicializacaoQueFunciona = new ClassePai
            {
                Propriedade = new ClasseDaPropriedade
                {
                    ValorDoTeste = "FirulaDefinida"
                }

            };
            ClassePai inicializacaoQueCagaTudo = new ClassePai
            {
                Propriedade = {  }
            };

            Console.WriteLine(inicializacaoQueFunciona.Propriedade.ValorDoTeste);
            Console.WriteLine(inicializacaoQueCagaTudo.Propriedade.ValorDoTeste);
        }
    }

    class ClassePai
    {
        public ClasseDaPropriedade Propriedade { get; set; }
    }

    class ClasseDaPropriedade
    {
        public ClasseDaPropriedade()
        {
            ValorDoTeste = "Firula";
        }
        public string ValorDoTeste { get; set; }
    }
}

