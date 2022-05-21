using System.Text.Json;

namespace ConfigurationDeserealizationTests;

public class IEnumerableDeserializedFromSystemTextJsonTests
{
    private readonly string JsonFromTheFile;

    private readonly IEnumerable<string> _expectedConfiguredValue = new []{ "sample1", "sample2", "sample3" };
    private readonly IEnumerable<string> _expectedDefaultValue = new string[]{};
    private readonly ConfigurationClass _deserializedJsonViaSystemTextJson;
    public IEnumerableDeserializedFromSystemTextJsonTests()
    {
        var fileInfo = new FileInfo("appsettings.ienumerable.json");
        JsonFromTheFile = fileInfo.OpenText().ReadToEndAsync().Result;
        var deserializedConfiguration = JsonSerializer.Deserialize<ConfigurationClass>(JsonFromTheFile);

        if(deserializedConfiguration == null)
            throw new Exception("Configuration should not be null");

        _deserializedJsonViaSystemTextJson = deserializedConfiguration;
    }

    [Fact]
    public void UnsetAndNotInitialized_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaSystemTextJson.UnsetAndNotInitialized.Should().BeNull();
    }

    [Fact]
    public void UnsetAndInitializedOnConstructor_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaSystemTextJson.UnsetAndInitializedOnConstructor.Should().BeEquivalentTo(_expectedDefaultValue);
    }

    [Fact]
    public void UnsetAndInitializedDirectly_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaSystemTextJson.UnsetAndInitializedDirectly.Should().BeEquivalentTo(_expectedDefaultValue);
    }

    [Fact]
    public void ConfiguredAndNotInitialized_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaSystemTextJson.ConfiguredAndNotInitialized.Should().BeEquivalentTo(_expectedConfiguredValue);
    }

    [Fact]
    public void ConfiguredAndInitializedOnConstructor_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaSystemTextJson.ConfiguredAndInitializedOnConstructor.Should().BeEquivalentTo(_expectedConfiguredValue);
    }

    [Fact]
    public void ConfiguredAndInitializedDirectly_DeserializedFromNewtonsoftJson_ShouldHaveAllValuesAsExpected()
    {
        _deserializedJsonViaSystemTextJson.ConfiguredAndInitializedDirectly.Should().BeEquivalentTo(_expectedConfiguredValue);
    }
}

