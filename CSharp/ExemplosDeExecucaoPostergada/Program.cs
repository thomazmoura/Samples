CasoDeExemplo casoDeExemplo;
if (!Enum.TryParse<CasoDeExemplo>(args[0], out casoDeExemplo))
    casoDeExemplo = CasoDeExemplo.ListaSimplesAleatoria;

ExecutarCasoDeExemplo(casoDeExemplo);

Console.WriteLine("Quantidade de chamadas realizadas:");
var resumoDeChamadas = UsuarioRastreavel.ContadorDeChamadas
    .Select(contador => $"{contador.Key}: {contador.Value}");
foreach(var resumoDeChamada in resumoDeChamadas)
    Console.WriteLine(resumoDeChamada);


void ExecutarCasoDeExemplo(CasoDeExemplo casoDeExemplo)
{
    var geradorDeExemplos = new GeradorDeExemplos();
    switch(casoDeExemplo)
    {
        default:
        case CasoDeExemplo.ListaSimplesAleatoria:
            var dadosPraCriacao = Enumerable.Range(0, 10)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO());
            var exemplos = geradorDeExemplos.InserirUsuariosRastreaveis(dadosPraCriacao);
            MostrarDadosDeExemplos(exemplos);
            break;

        case CasoDeExemplo.ComFiltrosSimples:
            var dadosPraCriacaoComFiltro = Enumerable.Range(0, 10)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO());
            var exemplosAcimaDaIdade = geradorDeExemplos.InserirUsuariosRastreaveis(dadosPraCriacaoComFiltro)
                .Ativos()
                .CujoNomeContenha("Silva")
                .MaioresDeIdade();
            MostrarDadosDeExemplos(exemplosAcimaDaIdade);
            break;
        case CasoDeExemplo.AlimentacaoComForeachELista:
            var listaDeExemplo = new List<UsuarioRastreavel>();
            var listaDeDadosDeCriacao = new List<CriacaoDeUsuarioRastreavelDTO>();
            for(var i = 0; i <= 5000; i++)
                listaDeDadosDeCriacao.Add(new CriacaoDeUsuarioRastreavelDTO());
            foreach(var dadosDeCriacao in listaDeDadosDeCriacao)
            {
                var usuario = new UsuarioRastreavel();
                usuario.NomeCompleto = dadosDeCriacao.NomeCompleto ?? geradorDeExemplos.GerarNome();
                usuario.Ativo = dadosDeCriacao.Ativo ?? geradorDeExemplos.GerarBooleano();
                usuario.DataDeNascimento = dadosDeCriacao.DataDeNascimento ?? geradorDeExemplos.GerarDataDeNascimento();
                listaDeExemplo.Add(usuario);
            }
            MostrarDadosDeExemplos(listaDeExemplo);
            break;
        case CasoDeExemplo.AlimentacaoComSelect:
            var usuariosDeExemplo = Enumerable.Range(0, 5000)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO())
                .Select(dto => new UsuarioRastreavel()
                {
                    NomeCompleto = dto.NomeCompleto ?? geradorDeExemplos.GerarNome(),
                    Ativo = dto.Ativo ?? geradorDeExemplos.GerarBooleano(),
                    DataDeNascimento = dto.DataDeNascimento ?? geradorDeExemplos.GerarDataDeNascimento()
                });
            MostrarDadosDeExemplos(usuariosDeExemplo);
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
