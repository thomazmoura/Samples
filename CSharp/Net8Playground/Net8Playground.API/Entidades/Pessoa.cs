namespace Net8Playground.API.Entidades;

public class Pessoa
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required Ocupacao Ocupacao { get; set; }
}

public class Ocupacao
{
    public required string Cargo { get; set; }
    public decimal ValorDaHora { get; set; }
    public required DateTime DataDeInicio { get; set; }
    public required Empresa Empresa { get; set; }
}

public class Empresa
{
    public required string Nome { get; set; }
}
