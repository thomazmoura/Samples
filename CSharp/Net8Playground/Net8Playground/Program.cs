IEnumerable<int> notasPrimeiroBimestre = [9, 8, 7];
int[] notasSegundoBimestre = [6, 5, 4];
var novoObjeto = new Objeto()
{
    Nome = "Teste",
    Quantidade = 5,
    Notas = [.. notasPrimeiroBimestre, .. notasSegundoBimestre]
};
Console.WriteLine($"Objeto: {JsonSerializer.Serialize(novoObjeto)}");

var serializacaoDeTeste = /*lang=json,strict*/ """
{
    "Nome":"Teste",
    "Quantidade":5,
    "Notas":[9,8,7,6,5,4]}
""";

var objetoDesserializado = JsonSerializer.Deserialize<Objeto>(serializacaoDeTeste)
    ?? throw new InvalidOperationException("O json não pode ser nulo");

Console.WriteLine($"{Environment.NewLine}Objeto com proprieades obrigatórias:");
Console.WriteLine($"Nome do objeto: {objetoDesserializado.Nome}");
Console.WriteLine($"Quantidade do objeto: {objetoDesserializado.Quantidade}");
Console.WriteLine($"Notas do objeto: {string.Join(", ", objetoDesserializado.Notas)}");

var objetoDesserializadoComConstrutor = JsonSerializer.Deserialize<ObjetoComConstrutor>(serializacaoDeTeste)
    ?? throw new InvalidOperationException("O json não pode ser nulo");

Console.WriteLine($"{Environment.NewLine}Objeto com Construtor obrigatório:");
Console.WriteLine($"Nome do objeto: {objetoDesserializadoComConstrutor.Nome}");
Console.WriteLine($"Quantidade do objeto: {objetoDesserializadoComConstrutor.Quantidade}");
Console.WriteLine($"Notas do objeto: {string.Join(", ", objetoDesserializadoComConstrutor.Notas)}");

var objetoDesserializadoComConstrutorPrimario = JsonSerializer.Deserialize<ObjetoComConstrutorPrimario>(serializacaoDeTeste)
    ?? throw new InvalidOperationException("O json não pode ser nulo");

Console.WriteLine($"{Environment.NewLine}Objeto com Construtor primário:");
Console.WriteLine($"Nome do objeto: {objetoDesserializadoComConstrutorPrimario.Nome}");
Console.WriteLine($"Quantidade do objeto: {objetoDesserializadoComConstrutorPrimario.Quantidade}");
Console.WriteLine($"Notas do objeto: {string.Join(", ", objetoDesserializadoComConstrutorPrimario.Notas)}");

public class Objeto
{
    public required string Nome { get; set; }
    public required int Quantidade { get; set; }
    public required IEnumerable<int> Notas { get; set; }
}

public class ObjetoComConstrutor
{
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public IEnumerable<int> Notas { get; set; }

    public ObjetoComConstrutor(string nome, int quantidade, IEnumerable<int> notas)
    {
        Nome = nome;
        Quantidade = quantidade;
        Notas = notas;
    }
}

public class ObjetoComConstrutorPrimario(string nome, int quantidade, IEnumerable<int> notas)
{
    public string Nome { get; set; } = nome;
    public int Quantidade { get; set; } = quantidade;
    public IEnumerable<int> Notas { get; set; } = notas;
}

public record ObjetoDTO(string Nome, int Quantidade, IEnumerable<int> Notas);
