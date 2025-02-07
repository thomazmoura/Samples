namespace ExemplosDeSincronismo.Servicos;

public interface IGarantidorDeDados
{
    public Task GarantirQueHaDadosNaBase(CancellationToken cancellationToken);
}

