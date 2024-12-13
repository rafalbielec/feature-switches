using OpenFeature;
using OpenFeature.Model;
using Refit;

namespace OpenFeatureApp;

public sealed class ApiProvider(string baseUri) : FeatureProvider
{
    private const string Name = "API Feature Provider";

    public interface IFlagsApi
    {
        [Get("/flags/bool/{name}")]
        Task<bool> GetBoolAsync(string name);
    }

    private readonly IFlagsApi _api = RestService.For<IFlagsApi>(baseUri);

    public override Metadata? GetMetadata()
    {
        return new Metadata(Name);
    }

    public override async Task<ResolutionDetails<bool>> ResolveBooleanValueAsync(string flagKey, bool defaultValue, EvaluationContext? context = null, CancellationToken cancellationToken = default)
    {
        var value = await _api.GetBoolAsync(flagKey);
        Console.WriteLine(value);
        return new ResolutionDetails<bool>(flagKey, value);
    }

    public override Task<ResolutionDetails<double>> ResolveDoubleValueAsync(string flagKey, double defaultValue, EvaluationContext? context = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<ResolutionDetails<int>> ResolveIntegerValueAsync(string flagKey, int defaultValue, EvaluationContext? context = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<ResolutionDetails<string>> ResolveStringValueAsync(string flagKey, string defaultValue, EvaluationContext? context = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<ResolutionDetails<Value>> ResolveStructureValueAsync(string flagKey, Value defaultValue, EvaluationContext? context = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}