namespace ExemplosDeIEnumerableEIQueryable.Dados;

public class ContextoDeExemplo : DbContext
{
    public ContextoDeExemplo(DbContextOptions<ContextoDeExemplo> options) : base(options) { }
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
}
