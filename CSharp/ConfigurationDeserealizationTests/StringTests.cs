namespace ConfigurationDeserealizationTests;

public class StringConfigurationTests
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.string.json")
        .Build();

    private readonly string _expectedConfiguredValue = "Set value";
    private readonly string _expectedDefaultValue = "Default value";
    private readonly ConfigurationClass _deserializedConfiguration;
    public StringConfigurationTests()
    {
        _deserializedConfiguration = _configuration
            .GetSection(nameof(ConfigurationClass))
            .Get<ConfigurationClass>();
    }

    [Fact]
    public void ConfiguredString_WhenDeserialized_ShouldBeExpectedValue()
    {
        _deserializedConfiguration.ConfiguredString.Should().Be(_expectedConfiguredValue);
    }

    [Fact]
    public void NullableConfiguredString_WhenDeserialized_ShouldBeExpectedValue()
    {
        _deserializedConfiguration.NullableConfiguredString.Should().Be(_expectedConfiguredValue);
    }

    [Fact]
    public void UnsetString_WhenDeserialized_ShouldBeExpectedValue()
    {
        _deserializedConfiguration.UnsetString.Should().Be(_expectedDefaultValue);
    }

    [Fact]
    public void NullableUnsetString_WhenDeserialized_ShouldBeExpectedValue()
    {
        _deserializedConfiguration.NullableUnsetString.Should().Be(_expectedDefaultValue);
    }

    private class ConfigurationClass
    {
        public ConfigurationClass()
        {
            UnsetString = "Default value";
            NullableUnsetString = "Default value";
        }

        public string ConfiguredString { get; set; }
        public string? NullableConfiguredString { get; set; }
        public string UnsetString { get; set; }
        public string? NullableUnsetString { get; set; }
    }
}


