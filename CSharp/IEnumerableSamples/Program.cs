namespace IEnumerableSamples;

public class Program
{
    public static void Main()
    {
        var listaDeObjetos = GerarExemplosDeClasse(new[] {
            true,
            false,
            false,
            true,
            false,
            false,
            false,
            true 
        });

        // 8 chamadas (precisa iterar todos pra ter a contagem final)
        //var resultado = listaDeObjetos
            //.Where(objeto => objeto.Ativo)
            //.Count();

        // 1 chamada (basta que chegue no primeiro true)
        //var resultado = listaDeObjetos
            //.Where(objeto => objeto.Ativo)
            //.Take(2)
            //.Any();

        // 2 chamadas (basta que chegue no primeiro false)
        var resultado = listaDeObjetos
            .Where(objeto => !objeto.Ativo)
            .Take(2)
            .Any();

        Console.WriteLine($"Quantidade de Itens: {listaDeObjetos.Count()}");
        Console.WriteLine($"Quantidade de Chamadas: {ExemploDeClasse.Contagem}");
        Console.WriteLine(resultado);

    }

    public static IEnumerable<ExemploDeClasse> GerarExemplosDeClasse(IEnumerable<bool> valoresBooleanos)
    {
        var exemplos = new List<ExemploDeClasse>();
        foreach (var booleano in valoresBooleanos)
            exemplos.Add(new ExemploDeClasse(){
                Ativo = booleano
            });
        return exemplos;
    }

    public static IEnumerable<ExemploDeClasse> GerarExemplosDeClasseComYield(IEnumerable<bool> valoresBooleanos)
    {
        foreach (var booleano in valoresBooleanos)
            yield return new ExemploDeClasse()
            {
                Ativo = booleano
            };
    }

    public static IEnumerable<ExemploDeClasse> GerarExemplosDeClasseComSelect(IEnumerable<bool> valoresBooleanos)
    {
        return valoresBooleanos
            .Select(booleano => new ExemploDeClasse()
            {
                Ativo = booleano
            });
    }

}

public class ExemploDeClasse
{
    public static int Contagem { get; set; }
    public bool _ativo;
    public bool Ativo
    {
        get {
            Contagem++;
            return _ativo; 
        }
        set { _ativo = value; }
    }


    public ExemploDeClasse() { }
}
