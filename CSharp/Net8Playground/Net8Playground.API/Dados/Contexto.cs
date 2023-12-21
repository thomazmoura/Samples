namespace Net8Playground.API.Dados;

public class Contexto : DbContext
{
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();

    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>()
            .OwnsOne(pessoa => pessoa.Ocupacao, builder =>
            {
                builder.ToJson();
                builder
                    .Property(ocupacao => ocupacao.ValorDaHora)
                    .HasPrecision(18, 2);
                builder.OwnsOne(ocupacao => ocupacao.Empresa);
            });
    }
}
