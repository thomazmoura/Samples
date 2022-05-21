using Newtonsoft.Json;

namespace ConfigurationDeserealizationTests;

public class IEnumerableDeserializedFromNewtonsoftTests
{
    private readonly string JsonFromTheFile;

    private readonly IEnumerable<string> _expectedConfiguredValue = new []{ "sample1", "sample2", "sample3" };
    private readonly IEnumerable<string> _expectedDefaultValue = new string[]{};
    private readonly ConfigurationClass _deserializedJsonViaNewtonsoft;
    public IEnumerableDeserializedFromNewtonsoftTests()
    {
        var fileInfo = new FileInfo("appsettings.ienumerable.json");
        JsonFromTheFile = fileInfo.OpenText().ReadToEndAsync().Result;
        _deserializedJsonViaNewtonsoft = JsonConvert.DeserializeObject<ConfigurationClass>(JsonFromTheFile);
    }

    [Fact]
    public void UnsetAndNotInitialized_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaNewtonsoft.UnsetAndNotInitialized.Should().BeNull();
    }

    [Fact]
    public void UnsetAndInitializedOnConstructor_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaNewtonsoft.UnsetAndInitializedOnConstructor.Should().BeEquivalentTo(_expectedDefaultValue);
    }

    [Fact]
    public void UnsetAndInitializedDirectly_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaNewtonsoft.UnsetAndInitializedDirectly.Should().BeEquivalentTo(_expectedDefaultValue);
    }

    [Fact]
    public void ConfiguredAndNotInitialized_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaNewtonsoft.ConfiguredAndNotInitialized.Should().BeEquivalentTo(_expectedConfiguredValue);
    }

    [Fact]
    public void ConfiguredAndInitializedOnConstructor_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaNewtonsoft.ConfiguredAndInitializedOnConstructor.Should().BeEquivalentTo(_expectedConfiguredValue);
    }

    [Fact]
    public void ConfiguredAndInitializedDirectly_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaNewtonsoft.ConfiguredAndInitializedDirectly.Should().BeEquivalentTo(_expectedConfiguredValue);
    }
}

