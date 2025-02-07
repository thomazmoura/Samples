namespace ExemplosDeSincronismo.Servicos;

public interface IExemplosDeSincronismoServico
{
    Task ExecutarAsync(CancellationToken cancellationToken);
}
