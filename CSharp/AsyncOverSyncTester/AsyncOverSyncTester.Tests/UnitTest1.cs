namespace AsyncOverSyncTester.Tests;

public class UnitTest1
{
    public UnitTest1(ClasseQualquer classeQualquer)
    {
        var teste = "abc";
        teste.Should().Be(classeQualquer.Propriedade);
    }

    [Theory]
    [InlineData(5, 5, "abc")]
    public void Test1(int teste, int outroTeste, string whatever)
    {
        var numero = teste;
        numero.Should().Be(teste);
    }
}

public class ClasseQualquer
{
    public string Propriedade { get; set; } = "";
}
