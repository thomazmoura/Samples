namespace ConfigurationDeserealizationTests;

public class StringSetsConfigurationTests
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.string-sets.json")
        .Build();

    private readonly ConfigurationClass _deserializedConfiguration;

    public StringSetsConfigurationTests()
    {
        _deserializedConfiguration = _configuration
            .GetSection(nameof(ConfigurationClass))
            .Get<ConfigurationClass>();
    }

    [Fact]
    public void IEnumerableItems_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.IEnumerableItems.Should().NotBeEmpty();
    }

    [Fact]
    public void ICollectionItems_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.ICollectionItems.Should().NotBeEmpty();
    }

    [Fact]
    public void IListItems_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.IListItems.Should().NotBeEmpty();
    }

    [Fact(Skip="This test actually fail indicating it's better not to initialize in this case")]
    public void InitializedIEnumerableItems_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.InitializedIEnumerableItems.Should().NotBeEmpty();
    }

    [Fact(Skip="This test actually fail indicating it's better not to initialize in this case")]
    public void InitializedICollectionItems_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.InitializedICollectionItems.Should().NotBeEmpty();
    }

    [Fact(Skip="This test actually fail indicating it's better not to initialize in this case")]
    public void InitializedIListItems_WhenDeserialized_ShouldNotBeEmpty()
    {
        _deserializedConfiguration.InitializedIListItems.Should().NotBeEmpty();
    }

    private class ConfigurationClass
    {
        public ConfigurationClass()
        {
            InitializedIEnumerableItems = Enumerable.Empty<string>();
            InitializedICollectionItems = new List<string>();
            InitializedIListItems = new List<string>();
        }

        public IEnumerable<string>? IEnumerableItems { get; set; }
        public ICollection<string>? ICollectionItems { get; set; }
        public IList<string>? IListItems { get; set; }
        public IEnumerable<string> InitializedIEnumerableItems { get; set; }
        public ICollection<string> InitializedICollectionItems { get; set; }
        public IList<string> InitializedIListItems { get; set; }
    }
}


