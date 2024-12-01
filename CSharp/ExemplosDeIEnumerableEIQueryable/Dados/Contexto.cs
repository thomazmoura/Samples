namespace ExemplosDeIEnumerableEIQueryable.Dados;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
}
