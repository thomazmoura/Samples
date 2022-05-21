namespace ConfigurationDeserealizationTests;

public class ConfigurationClass
{
    public ConfigurationClass()
    {
        ConfiguredAndInitializedOnConstructor = Enumerable.Empty<string>();
        UnsetAndInitializedOnConstructor = Enumerable.Empty<string>();
    }

    public IEnumerable<string> ConfiguredAndInitializedOnConstructor { get; set; }
    public IEnumerable<string> ConfiguredAndInitializedDirectly { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<string>? ConfiguredAndNotInitialized { get; set; }
    public IEnumerable<string> UnsetAndInitializedOnConstructor { get; set; }
    public IEnumerable<string> UnsetAndInitializedDirectly { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<string>? UnsetAndNotInitialized { get; set; }
}
