public class UsuarioRastreavel
{
    public static IDictionary<string, int> ContadorDeChamadas { get; private set; } = new Dictionary<string, int>(){
        { nameof(UsuarioRastreavel), 0 },
        { nameof(Id), 0 },
        { nameof(Ativo), 0 },
        { nameof(DataDeNascimento), 0 },
        { nameof(NomeCompleto), 0 },
    };

    public UsuarioRastreavel()
    {
        ContadorDeChamadas[nameof(UsuarioRastreavel)]++;
    }

    private Guid _id = Guid.NewGuid();
    public Guid Id {
        get { ContadorDeChamadas[nameof(Id)]++; return _id; }
    }

    private bool _ativo;
    public bool Ativo {
        get { ContadorDeChamadas[nameof(Ativo)]++; return _ativo; }
        set { _ativo = value; }
    }

    private DateTime _dataDeNascimento;
    public DateTime DataDeNascimento {
        get { ContadorDeChamadas[nameof(DataDeNascimento)]++; return _dataDeNascimento; }
        set { _dataDeNascimento = value; }
    }

    private string _nomeCompleto = String.Empty;
    public string NomeCompleto {
        get { ContadorDeChamadas[nameof(NomeCompleto)]++; return _nomeCompleto; }
        set { _nomeCompleto = value; }
    }
}

public static class UsuarioRastreavelExtensions
{
    public static IEnumerable<UsuarioRastreavel> MaioresDeIdade(this IEnumerable<UsuarioRastreavel> usuarios)
    {
        return usuarios.Where(usuario => (DateTime.UtcNow - usuario.DataDeNascimento).TotalDays / 365 >= 18);
    }

    public static IEnumerable<UsuarioRastreavel> CujoNomeContenha(this IEnumerable<UsuarioRastreavel> usuarios, string trechoDoNome)
    {
        return usuarios.Where(usuario => usuario.NomeCompleto.Contains(trechoDoNome));
    }

    public static IEnumerable<UsuarioRastreavel> Ativos(this IEnumerable<UsuarioRastreavel> usuarios)
    {
        return usuarios.Where(usuario => usuario.Ativo);
    }
}
