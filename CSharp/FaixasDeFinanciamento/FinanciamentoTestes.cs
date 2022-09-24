namespace FaixasDeFinanciamento;

public class FinanciamentoTestes
{
    [Theory]
    [MemberData(nameof(CasosDeTesteComFaixaUnitaria))]
    public void ObterValorPorQuantidadeDeParcelas_QuandoAParcelaSeEncaixaEmUmaFaixaUnitária_RetornaOValorDaFaixa(
        int quantidadeDeParcelas, decimal valorEsperado, IEnumerable<FaixaDeParcelas> faixaDeParcelas
    )
    {
        var faturamento = new Financiamento()
        {
            FaixaDeParcelas = faixaDeParcelas
        };

        var valorObtido = faturamento.ObterValorPorQuantidadeDeParcelas(quantidadeDeParcelas);

        valorObtido.Should().Be(valorEsperado);
    }

    [Theory]
    [MemberData(nameof(CasosDeTesteComFaixaComIntervalos))]
    public void ObterValorPorQuantidadeDeParcelas_QuandoAParcelaSeEncaixaEmUmaFaixaComIntervalos_RetornaOValorDaFaixa(
        int quantidadeDeParcelas, decimal valorEsperado, IEnumerable<FaixaDeParcelas> faixaDeParcelas
    )
    {
        var faturamento = new Financiamento()
        {
            FaixaDeParcelas = faixaDeParcelas
        };

        var valorObtido = faturamento.ObterValorPorQuantidadeDeParcelas(quantidadeDeParcelas);

        valorObtido.Should().Be(valorEsperado);
    }

    public static IEnumerable<object[]> CasosDeTesteComFaixaUnitaria => new []{
        new {
            QuantidadeDeParcelas = 1,
            ValorEsperado = 10m,
            FaixasDoTeste = new[]
            {
                new FaixaDeParcelas(1,1, 10m),
                new FaixaDeParcelas(2,2, 20m),
            }
        },
        new {
            QuantidadeDeParcelas = 2,
            ValorEsperado = 20m,
            FaixasDoTeste = new[]
            {
                new FaixaDeParcelas(1,1, 10m),
                new FaixaDeParcelas(2,2, 20m),
            }
        },
    }.Select(dados => new object[] { dados.QuantidadeDeParcelas, dados.ValorEsperado, dados.FaixasDoTeste });

    public static IEnumerable<object[]> CasosDeTesteComFaixaComIntervalos => new []{
        new {
            QuantidadeDeParcelas = 1,
            ValorEsperado = 100m,
            FaixasDoTeste = new[]
            {
                new FaixaDeParcelas(1,5, 100m),
                new FaixaDeParcelas(6,12, 200m),
            }
        },
        new {
            QuantidadeDeParcelas = 3,
            ValorEsperado = 100m,
            FaixasDoTeste = new[]
            {
                new FaixaDeParcelas(1,5, 100m),
                new FaixaDeParcelas(6,12, 200m),
            }
        },
        new {
            QuantidadeDeParcelas = 5,
            ValorEsperado = 100m,
            FaixasDoTeste = new[]
            {
                new FaixaDeParcelas(1,5, 100m),
                new FaixaDeParcelas(6,12, 200m),
            }
        },
        new {
            QuantidadeDeParcelas = 6,
            ValorEsperado = 200m,
            FaixasDoTeste = new[]
            {
                new FaixaDeParcelas(1,5, 100m),
                new FaixaDeParcelas(6,12, 200m),
            }
        },
        new {
            QuantidadeDeParcelas = 10,
            ValorEsperado = 200m,
            FaixasDoTeste = new[]
            {
                new FaixaDeParcelas(1,5, 100m),
                new FaixaDeParcelas(6,12, 200m),
            }
        },
        new {
            QuantidadeDeParcelas = 12,
            ValorEsperado = 200m,
            FaixasDoTeste = new[]
            {
                new FaixaDeParcelas(1,5, 100m),
                new FaixaDeParcelas(6,12, 200m),
            }
        },
    }.Select(dados => new object[] { dados.QuantidadeDeParcelas, dados.ValorEsperado, dados.FaixasDoTeste });
    
}
