﻿namespace ExemplosDeSincronismo.Entidades;

public class Pessoa
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public string? Apelido { get; set; }
    public required bool Ativo { get; set; }
    public required DateTime DataDeNascimento { get; set; }
    public IEnumerable<Pessoa> PessoasAtivasComMesmaDataDeAniversario { get; set; } = [];
    public byte[] Version { get; set; } = [];

    public virtual ICollection<Compra> Compras { get; set; } = [];
}
