CasoDeExemplo casoDeExemplo;
string entradaDeCasoDeExemplo;
var quantidadeDeExemplos = 5000000;

if(!args.Any())
{
    var descricaoDosEnums = Enum.GetValues<CasoDeExemplo>().Select(caso => $"\r\n * {caso}({(int)caso})").ToList();
    var listaDeCasosDeExemplo = String.Join(", ", descricaoDosEnums);
    Console.WriteLine($"Escolha o exemplo:{listaDeCasosDeExemplo}");
    entradaDeCasoDeExemplo = Console.ReadLine() ?? CasoDeExemplo.ListaSimplesAleatoria.ToString();
}
else
{
    entradaDeCasoDeExemplo = args[0];
}

if (!Enum.TryParse<CasoDeExemplo>(entradaDeCasoDeExemplo, out casoDeExemplo))
    casoDeExemplo = CasoDeExemplo.ListaSimplesAleatoria;

ExecutarCasoDeExemplo(casoDeExemplo);

Console.WriteLine("Quantidade de chamadas realizadas:");
var resumoDeChamadas = UsuarioRastreavel.ContadorDeChamadas
    .Select(contador => $"{contador.Key}: {contador.Value}");
foreach(var resumoDeChamada in resumoDeChamadas)
    Console.WriteLine(resumoDeChamada);

Console.ReadKey();

void ExecutarCasoDeExemplo(CasoDeExemplo casoDeExemplo)
{
    var geradorDeExemplos = new GeradorDeExemplos();
    switch(casoDeExemplo)
    {
        default:
        case CasoDeExemplo.ListaSimplesAleatoria:
            var dadosPraCriacao = Enumerable.Range(0, 10)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO());
            var exemplos = geradorDeExemplos.ObterUsuariosRastreaveis(dadosPraCriacao);
            MostrarDadosDeExemplos(exemplos);
            break;

        case CasoDeExemplo.ComFiltrosSimples:
            var dadosPraCriacaoComFiltro = Enumerable.Range(0, 10)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO());
            var exemplosAcimaDaIdade = geradorDeExemplos.ObterUsuariosRastreaveis(dadosPraCriacaoComFiltro)
                .Ativos()
                .CujoNomeContenha("Silva")
                .MaioresDeIdade();
            MostrarDadosDeExemplos(exemplosAcimaDaIdade);
            break;
        case CasoDeExemplo.AlimentacaoComForeachELista:
            var listaDeExemplo = new List<UsuarioRastreavel>();
            var listaDeDadosDeCriacao = new List<CriacaoDeUsuarioRastreavelDTO>();
            for(var i = 0; i < quantidadeDeExemplos; i++)
                listaDeDadosDeCriacao.Add(new CriacaoDeUsuarioRastreavelDTO());
            foreach(var dadosDeCriacao in listaDeDadosDeCriacao)
            {
                var usuario = new UsuarioRastreavel();
                usuario.NomeCompleto = dadosDeCriacao.NomeCompleto ?? geradorDeExemplos.GerarNome();
                usuario.Ativo = dadosDeCriacao.Ativo ?? geradorDeExemplos.GerarBooleano();
                usuario.DataDeNascimento = dadosDeCriacao.DataDeNascimento ?? geradorDeExemplos.GerarDataDeNascimento();
                listaDeExemplo.Add(usuario);
            }
            break;
        case CasoDeExemplo.AlimentacaoComSelect:
            var usuariosDeExemplo = Enumerable.Range(0, quantidadeDeExemplos)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO())
                .Select(dto => new UsuarioRastreavel()
                {
                    NomeCompleto = dto.NomeCompleto ?? geradorDeExemplos.GerarNome(),
                    Ativo = dto.Ativo ?? geradorDeExemplos.GerarBooleano(),
                    DataDeNascimento = dto.DataDeNascimento ?? geradorDeExemplos.GerarDataDeNascimento()
                }).ToList();
            break;
        case CasoDeExemplo.AlimentacaoComFiltrosPostergada:
            var dadosPraCriacaoComFiltroPostergado = Enumerable.Range(0, quantidadeDeExemplos)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO()).ToList();
            var exemplosFiltradosPostergados = geradorDeExemplos.ObterUsuariosRastreaveis(dadosPraCriacaoComFiltroPostergado)
                .Ativos()
                .CujoNomeContenha("Silva")
                .MaioresDeIdade()
                .ToList();
            break;
        case CasoDeExemplo.AlimentacaoComFiltrosForcada:
            var dadosPraCriacaoComFiltroForcado = Enumerable.Range(0, quantidadeDeExemplos)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO()).ToList();
            var exemplosFiltradosForcados = geradorDeExemplos.ObterUsuariosRastreaveis(dadosPraCriacaoComFiltroForcado)
                .Ativos().ToList()
                .CujoNomeContenha("Silva").ToList()
                .MaioresDeIdade().ToList()
                .ToList();
            break;
        case CasoDeExemplo.AlimentacaoComFiltrosPostergadaELimitado:
            var dadosPraCriacaoComFiltroPostergadoELimitado = Enumerable.Range(0, quantidadeDeExemplos)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO()).ToList();
            var exemplosFiltradosPostergadosELimitados = geradorDeExemplos
                .ObterUsuariosRastreaveis(dadosPraCriacaoComFiltroPostergadoELimitado)
                .Ativos()
                .CujoNomeContenha("Silva")
                .MaioresDeIdade()
                .Take(300).ToList();
            break;
        case CasoDeExemplo.AlimentacaoComFiltrosForcadaELimitado:
            var dadosPraCriacaoComFiltroForcadoELimitado = Enumerable.Range(0, quantidadeDeExemplos)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO()).ToList();
            var exemplosFiltradosForcadoELimitados = geradorDeExemplos.ObterUsuariosRastreaveis(dadosPraCriacaoComFiltroForcadoELimitado)
                .Ativos().ToList()
                .CujoNomeContenha("Silva").ToList()
                .MaioresDeIdade().ToList()
                .Take(300).ToList();
            break;
        case CasoDeExemplo.AlimentacaoComFiltrosPostergadaAny:
            var dadosPraCriacaoComFiltroPostergadoAny = Enumerable.Range(0, quantidadeDeExemplos)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO()).ToList();
            var exemplosFiltradosPostergadosAny = geradorDeExemplos
                .ObterUsuariosRastreaveis(dadosPraCriacaoComFiltroPostergadoAny)
                .Ativos()
                .CujoNomeContenha("Silva")
                .MaioresDeIdade()
                .Any();
            break;
        case CasoDeExemplo.AlimentacaoComFiltrosForcadaAny:
            var dadosPraCriacaoComFiltroForcadoAny = Enumerable.Range(0, quantidadeDeExemplos)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO()).ToList();
            var exemplosFiltradosForcadoAny = geradorDeExemplos.ObterUsuariosRastreaveis(dadosPraCriacaoComFiltroForcadoAny)
                .Ativos().ToList()
                .CujoNomeContenha("Silva").ToList()
                .MaioresDeIdade().ToList()
                .Any();
            break;
    }
}

void MostrarDadosDeExemplos(IEnumerable<UsuarioRastreavel> exemplos)
{
    Console.WriteLine("Exemplos:");
    foreach(var exemplo in exemplos)
    {
        var resumoDoExemplo = $"Nome Completo: {exemplo.NomeCompleto}; Ativo: {exemplo.Ativo}; Data de Nascimento: {exemplo.DataDeNascimento}";
        Console.WriteLine(resumoDoExemplo);
    }
}
