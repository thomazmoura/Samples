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
    public void InitializedTimeSpan_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.InitializedTimeSpan.Should().Be(_expectedConfiguredTimeSpan);
    }

    [Fact]
    public void NullableInitializedTimeSpan_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.NullableInitializedTimeSpan.Should().Be(_expectedConfiguredTimeSpan);
    }

    [Fact]
    public void TimeSpan_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.TimeSpan.Should().Be(_expectedDefaultTimeSpan);
    }

    [Fact]
    public void NullableTimeSpan_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.NullableTimeSpan.Should().Be(_expectedDefaultTimeSpan);
    }

    private class ConfigurationClass
    {
        public ConfigurationClass()
        {
            TimeSpan = TimeSpan.FromMinutes(5);
            NullableTimeSpan = TimeSpan.FromMinutes(5);
        }

        public TimeSpan InitializedTimeSpan{ get; set; }
        public TimeSpan? NullableInitializedTimeSpan{ get; set; }
        public TimeSpan TimeSpan{ get; set; }
        public TimeSpan? NullableTimeSpan{ get; set; }
    }
}


