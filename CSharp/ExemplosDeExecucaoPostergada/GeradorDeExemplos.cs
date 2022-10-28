public class GeradorDeExemplos
{
    private readonly Faker _faker;
    public GeradorDeExemplos()
    {
        _faker = new Faker("pt_BR");
        Randomizer.Seed = new Random(42);
    }

    internal IEnumerable<UsuarioRastreavel> InserirUsuariosRastreaveis(IEnumerable<CriacaoDeUsuarioRastreavelDTO> criacaoDeUsuariosDTO)
    {
        return criacaoDeUsuariosDTO.Select(dto => new UsuarioRastreavel()
        {
            NomeCompleto = dto.NomeCompleto ?? _faker.Name.FullName(),
            Ativo = dto.Ativo ?? _faker.Random.Bool(),
            DataDeNascimento = dto.DataDeNascimento ?? _faker.Date.Past(yearsToGoBack: 100)
        });
    }
}

public record CriacaoDeUsuarioRastreavelDTO(DateTime? DataDeNascimento = null, bool? Ativo = null, string? NomeCompleto = null);

internal enum CasoDeExemplo
{
    ListaSimplesAleatoria,
    FiltroPorIdade,
}

