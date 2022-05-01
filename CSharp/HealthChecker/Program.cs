using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthChecker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var resposta = await httpClient.GetAsync("https://localhost:5001/saude");
            if(!resposta.IsSuccessStatusCode)
                throw new Exception("It didn't work");

            var resultado = await resposta.Content.ReadAsStringAsync();
            Console.WriteLine(resultado);

        }

        public class RetornoDeErro
        {
            public IEnumerable<string> MensagensDeErroGerais { get; set; }
            public IDictionary<string, ICollection<string>> MensagensDeErroPorCampo { get; set; }
            public IEnumerable<string> TodasAsMensagensDeErro { get; set; }
        }
    }
}
