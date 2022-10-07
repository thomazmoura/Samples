namespace FaixasDeFinanciamento;

public class Financiamento
{
    public string Descricao { get; init; } = "";
    public IEnumerable<FaixaDeParcelas> FaixaDeParcelas { get; init; } = Enumerable.Empty<FaixaDeParcelas>();

    public decimal ObterValorPorQuantidadeDeParcelas(int quantidadeDeParcelas)
    {
        return FaixaDeParcelas
            .Single(faixa => faixa.Inicio <= quantidadeDeParcelas && faixa.Final >= quantidadeDeParcelas)
            .Valor;
    }
}

public class ConfiguracaoQualquer
{
    private readonly ConfiguracaoPadrao configuracao;

    public ConfiguracaoQualquer(ConfiguracaoPadrao configuracao)
    {
        this.configuracao = configuracao;
    }
}

public record FaixaDeParcelas (int Inicio, int Final, decimal Valor);

public class ConfiguracaoPadrao
{
    public Financiamento FinanciamentoSimples => new Financiamento()
    {
        Descricao = "Financiamento Simples",
        FaixaDeParcelas = new []{
            new FaixaDeParcelas(1, 5, 30m),
            new FaixaDeParcelas(6, 11, 50m),
            new FaixaDeParcelas(12, 24, 120m)
        }
    };
    public Financiamento FinanciamentoDetalhado => new Financiamento()
    {
        Descricao = "Financiamento Demasiadamente Detalhado",
        FaixaDeParcelas = new []{
            new FaixaDeParcelas(1, 1, 5),
            new FaixaDeParcelas(2, 2, 10),
            new FaixaDeParcelas(3, 3, 15),
            new FaixaDeParcelas(4, 4, 20),
            new FaixaDeParcelas(5, 5, 25),
            new FaixaDeParcelas(6, 11, 100),
            new FaixaDeParcelas(12, 24, 250)
        }
    };
}
