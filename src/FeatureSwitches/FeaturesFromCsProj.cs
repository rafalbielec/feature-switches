using System.Diagnostics.CodeAnalysis;

namespace FeatureSwitches;

internal static class FeaturesFromCsProj
{
    [FeatureSwitchDefinition(FeatureNames.FeatureOne)]
    internal static bool One => AppContext.TryGetSwitch(FeatureNames.FeatureOne, out bool isEnabled) && isEnabled;

    [FeatureSwitchDefinition(FeatureNames.FeatureTwo)]
    internal static bool Two => AppContext.TryGetSwitch(FeatureNames.FeatureTwo, out bool isEnabled) && isEnabled;

    /// <summary>
    /// This should get trimmed by the compiler.
    /// </summary>
    internal static void AddFeatureTwoEndpoint(WebApplication app)
    {
        app.MapGet("/api/second", () => Results.Ok("Enabled in .csproj"));
    }

    /// <summary>
    /// This should also get trimmed by the compiler.
    /// </summary>
    internal static void TrimmedIfNotUsed() => Console.WriteLine("Feature on");
}