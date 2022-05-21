namespace ConfigurationDeserealizationTests;

public class StringSetsConfigurationTests
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.string-sets.json")
        .Build();

    private readonly IEnumerable<string> _expectedConfiguredValue = new []{ "sample1", "sample2", "sample3" };
    private readonly ConfigurationClass _deserializedConfiguration;
    public StringSetsConfigurationTests()
    {
        _deserializedConfiguration = _configuration
            .GetSection(nameof(ConfigurationClass))
            .Get<ConfigurationClass>();
    }

    [Fact]
    public void UnsetIEnumerableItems_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.UnsetIEnumerableItems.Should().NotBeNull();
    }

    [Fact]
    public void UnsetICollectionItems_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.UnsetICollectionItems.Should().NotBeNull();
    }

    [Fact]
    public void UnsetIListItems_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.UnsetIListItems.Should().NotBeNull();
    }

    [Fact]
    public void ConfiguredIEnumerableItems_WhenDeserialized_ShouldBeConfigureValue()
    {
        _deserializedConfiguration.ConfiguredIEnumerableItems.Should().BeEquivalentTo(_expectedConfiguredValue);
    }

    [Fact]
    public void ConfiguredICollectionItems_WhenDeserialized_ShouldBeConfigureValue()
    {
        _deserializedConfiguration.ConfiguredICollectionItems.Should().BeEquivalentTo(_expectedConfiguredValue);
    }

    [Fact]
    public void ConfiguredIListItems_WhenDeserialized_ShouldBeConfigureValue()
    {
        _deserializedConfiguration.ConfiguredIListItems.Should().BeEquivalentTo(_expectedConfiguredValue);
    }

    private class ConfigurationClass
    {
        public ConfigurationClass()
        {
            UnsetIEnumerableItems = Enumerable.Empty<string>();
            UnsetICollectionItems = new List<string>();
            UnsetIListItems = new List<string>();
            ConfiguredIEnumerableItems = Enumerable.Empty<string>();
            ConfiguredICollectionItems = new List<string>();
            ConfiguredIListItems = new List<string>();
        }

        public IEnumerable<string> UnsetIEnumerableItems { get; set; }
        public ICollection<string> UnsetICollectionItems { get; set; }
        public IList<string> UnsetIListItems { get; set; }
        public IEnumerable<string> ConfiguredIEnumerableItems { get; set; }
        public ICollection<string> ConfiguredICollectionItems { get; set; }
        public IList<string> ConfiguredIListItems { get; set; }
    }
}


