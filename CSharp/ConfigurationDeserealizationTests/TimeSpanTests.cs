namespace ConfigurationDeserealizationTests;

public class TimeSpanConfigurationTests
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.timespan.json")
        .Build();

    private readonly ConfigurationClass _deserializedConfiguration;
    private readonly TimeSpan _expectedDefaultTimeSpan;
    /// <summary> This is the value set on  appsettings.timespan.json </summary>
    private readonly TimeSpan _expectedConfiguredTimeSpan;
    public TimeSpanConfigurationTests()
    {
        _deserializedConfiguration = _configuration
            .GetSection(nameof(ConfigurationClass))
            .Get<ConfigurationClass>();
        _expectedDefaultTimeSpan = TimeSpan.FromMinutes(5);
        _expectedConfiguredTimeSpan = TimeSpan.FromMinutes(15);
    }

    [Fact]
    public void ConfiguredTimeSpan_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.ConfiguredTimeSpan.Should().Be(_expectedConfiguredTimeSpan);
    }

    [Fact]
    public void NullableConfiguredTimeSpan_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.NullableConfiguredTimeSpan.Should().Be(_expectedConfiguredTimeSpan);
    }

    [Fact]
    public void UnsetTimeSpan_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.UnsetTimeSpan.Should().Be(_expectedDefaultTimeSpan);
    }

    [Fact]
    public void NullableUnsetTimeSpan_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.NullableUnsetTimeSpan.Should().Be(_expectedDefaultTimeSpan);
    }

    private class ConfigurationClass
    {
        public ConfigurationClass()
        {
            UnsetTimeSpan = TimeSpan.FromMinutes(5);
            NullableUnsetTimeSpan = TimeSpan.FromMinutes(5);
        }

        public TimeSpan ConfiguredTimeSpan{ get; set; }
        public TimeSpan? NullableConfiguredTimeSpan{ get; set; }
        public TimeSpan UnsetTimeSpan{ get; set; }
        public TimeSpan? NullableUnsetTimeSpan{ get; set; }
    }
}


