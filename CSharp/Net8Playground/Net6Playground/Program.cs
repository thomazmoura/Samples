IEnumerable<int> notasPrimeiroBimestre = [9, 8, 7];
int[] notasSegundoBimestre = [6, 5, 4];
var novoObjeto = new Objeto()
{
    Nome = "Teste",
    Quantidade = 5,
    Notas = [.. notasPrimeiroBimestre, .. notasSegundoBimestre]
};
Console.WriteLine($"Objeto: {JsonSerializer.Serialize(novoObjeto)}");

public class Objeto
{
    public required string Nome { get; set; }
    public required int Quantidade { get; set; }
    public required IEnumerable<int> Notas { get; set; }
}

public record ObjetoDTO(string Nome, int Quantidade, IEnumerable<int> Notas);

