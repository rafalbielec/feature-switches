using Microsoft.FeatureManagement;

namespace FeatureSwitches;

internal class InMemoryFeatureProvider(params FeatureDefinition[] featureDefinitions) : IFeatureDefinitionProvider
{
    private readonly IEnumerable<FeatureDefinition> _definitions =
        featureDefinitions ?? throw new ArgumentNullException(nameof(featureDefinitions));

    public async IAsyncEnumerable<FeatureDefinition> GetAllFeatureDefinitionsAsync()
    {
        foreach (FeatureDefinition definition in _definitions)
        {
            yield return definition;
        }
    }

    public Task<FeatureDefinition?> GetFeatureDefinitionAsync(string featureName)
    {
        return Task.FromResult(_definitions.FirstOrDefault(definitions =>
                    definitions.Name.Equals(featureName, StringComparison.OrdinalIgnoreCase)));
    }
}