namespace ExemplosDeIEnumerableEIQueryable.Entidades;

public class Compra
{
    public int Id { get; set; }
    public required DateTime DataDaCompra { get; set; }

    public required int PessoaId { get; set; }

    public virtual Pessoa? Pessoa { get; set; }
    public virtual ICollection<ItemDaCompra> ItensDaCompra { get; set; } = [];
}

public class ItemDaCompra
{
    public int Id { get; set; }
    public int Quantidade { get; set; }

    public int CompraId { get; set; }
    public int ProdutoId { get; set; }

    public virtual Compra? Compra { get; set; }
    public virtual Produto? Produto { get; set; }
}

