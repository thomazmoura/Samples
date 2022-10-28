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

        case CasoDeExemplo.FiltroPorIdade:
            var dadosPraCriacaoComFiltro = Enumerable.Range(0, 10)
                .Select(_ => new CriacaoDeUsuarioRastreavelDTO());
            var exemplosAcimaDaIdade = geradorDeExemplos.InserirUsuariosRastreaveis(dadosPraCriacaoComFiltro)
                .MaioresDeIdade();
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
