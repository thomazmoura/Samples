namespace ExemplosDeIEnumerableEIQueryable.Dados;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Compra> Compras => Set<Compra>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compra>()
            .OwnsMany(compra => compra.ItensDaCompra, options =>
            {
                options.ToJson();
            });
    }
}
