public class GeradorDeExemplos
{
    private readonly Faker _faker;
    public GeradorDeExemplos()
    {
        _faker = new Faker("pt_BR");
        Randomizer.Seed = new Random(42);
    }

    internal string GerarNome() => _faker.Name.FullName();
    internal int GerarCodigo() => (int)_faker.Random.UInt();
    internal bool GerarBooleano() => _faker.Random.Bool();
    internal DateTime GerarDataDeNascimento() => _faker.Date.Past(yearsToGoBack: 100);
    internal IEnumerable<UsuarioRastreavel> ObterUsuariosRastreaveis(IEnumerable<CriacaoDeUsuarioRastreavelDTO> criacaoDeUsuariosDTO)
    {
        return criacaoDeUsuariosDTO.Select(dto => new UsuarioRastreavel()
        {
            NomeCompleto = dto.NomeCompleto ?? GerarNome(),
            Ativo = dto.Ativo ?? GerarBooleano(),
            DataDeNascimento = dto.DataDeNascimento ?? GerarDataDeNascimento()
        });
    }
}

public record CriacaoDeUsuarioRastreavelDTO(DateTime? DataDeNascimento = null, bool? Ativo = null, string? NomeCompleto = null);

internal enum CasoDeExemplo
{
    ListaSimplesAleatoria,
    ComFiltrosSimples,
    AlimentacaoComForeachELista,
    AlimentacaoComSelect,
    AlimentacaoComFiltrosPostergada,
    AlimentacaoComFiltrosForcada,
    AlimentacaoComFiltrosPostergadaELimitado,
    AlimentacaoComFiltrosForcadaELimitado,
    AlimentacaoComFiltrosPostergadaAny,
    AlimentacaoComFiltrosForcadaAny,
    AlimentacaoComFiltrosDeLista,
    AlimentacaoComFiltrosDeHashSet,
}

