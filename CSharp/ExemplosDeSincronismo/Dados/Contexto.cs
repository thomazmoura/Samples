namespace ExemplosDeSincronismo.Dados;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<PessoaStage1> PessoasStage1 => Set<PessoaStage1>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<ProdutoStage1> ProdutosStage1 => Set<ProdutoStage1>();
    public DbSet<Compra> Compras => Set<Compra>();
    public DbSet<ResultadoDeChecksum> Checksums => Set<ResultadoDeChecksum>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compra>()
            .OwnsMany(compra => compra.ItensDaCompra, options =>
            {
                options.ToJson();
            });
        modelBuilder.Entity<Pessoa>()
            .Property(pessoa => pessoa.Version)
            .IsRowVersion();
    }
}

public record ResultadoDeChecksum(int Id, int Checksum);
