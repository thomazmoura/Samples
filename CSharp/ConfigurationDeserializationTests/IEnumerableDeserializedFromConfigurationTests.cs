namespace ConfigurationDeserealizationTests;

public class IEnumerableDeserializedFromConfigurationTests
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.ienumerable.json")
        .Build();

    private readonly IEnumerable<string> _expectedConfiguredValue = new []{ "sample1", "sample2", "sample3" };
    private readonly IEnumerable<string> _expectedDefaultValue = new string[]{};
    private readonly ConfigurationClass _deserializedConfiguration;
    public IEnumerableDeserializedFromConfigurationTests()
    {
        _deserializedConfiguration = _configuration
            .GetSection(nameof(ConfigurationClass))
            .Get<ConfigurationClass>();
    }

    [Fact]
    public void UnsetAndNotInitialized_DeserializedFromTheConfigurationBuilder_ShouldBeNull()
    {
        _deserializedConfiguration.UnsetAndNotInitialized.Should().BeNull();
    }

    [Fact]
    public void UnsetAndInitializedOnConstructor_DeserializedFromTheConfigurationBuilder_ShouldHaveAllValuesAsDefault()
    {
        _deserializedConfiguration.UnsetAndInitializedOnConstructor.Should().BeEquivalentTo(_expectedDefaultValue);
    }

    [Fact]
    public void UnsetAndInitializedDirectly_DeserializedFromTheConfigurationBuilder_ShouldHaveAllValuesAsDefault()
    {
        _deserializedConfiguration.UnsetAndInitializedDirectly.Should().BeEquivalentTo(_expectedDefaultValue);
    }

    [Fact]
    public void ConfiguredAndNotInitialized_DeserializedFromTheConfigurationBuilder_ShouldHaveAllValuesAsConfigured()
    {
        _deserializedConfiguration.ConfiguredAndNotInitialized.Should().BeEquivalentTo(_expectedConfiguredValue);
    }

    [Fact]
    public void ConfiguredAndInitializedOnConstructor_DeserializedFromTheConfigurationBuilder_ShouldHaveAllValuesAsConfigured()
    {
        _deserializedConfiguration.ConfiguredAndInitializedOnConstructor.Should().BeEquivalentTo(_expectedConfiguredValue);
    }

    [Fact]
    public void ConfiguredAndInitializedDirectly_DeserializedFromTheConfigurationBuilder_ShouldHaveAllValuesAsConfigured()
    {
        _deserializedConfiguration.ConfiguredAndInitializedDirectly.Should().BeEquivalentTo(_expectedConfiguredValue);
    }
}

