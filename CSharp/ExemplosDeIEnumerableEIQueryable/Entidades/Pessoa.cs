﻿namespace ExemplosDeIEnumerableEIQueryable.Entidades;

public class Pessoa
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required bool Ativo { get; set; }
    public required DateTime DataDeNascimento { get; set; }
    public IEnumerable<Pessoa> PessoasAtivasComMesmaDataDeAniversario { get; set; } = [];
}