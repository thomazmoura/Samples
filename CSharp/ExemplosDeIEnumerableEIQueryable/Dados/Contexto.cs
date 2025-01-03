namespace ExemplosDeIEnumerableEIQueryable.Dados;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Compra> Compras => Set<Compra>();
    public DbSet<ItemDaCompra> ItemDaCompra => Set<ItemDaCompra>();
}
