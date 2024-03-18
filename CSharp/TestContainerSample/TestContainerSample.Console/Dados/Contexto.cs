namespace TestContainerSample.Console.Dados;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos => Set<Produto>();
}

