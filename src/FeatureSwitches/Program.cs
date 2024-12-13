using Microsoft.FeatureManagement;

namespace FeatureSwitches;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "yyyy-MM-dd HH:mm ";
        });

        builder.Services.AddOpenApi();

        // Let's build a provider in memory instead of using the config file
        var memoryProvideWithoutConfigFile = new InMemoryFeatureProvider(
           new FeatureDefinition
           {
               Name = FeatureNames.FeatureFromManager,
               EnabledFor = [
                   new FeatureFilterConfiguration() { Name = "AlwaysOn" }
               ]
           });

        builder.Services.AddSingleton<IFeatureDefinitionProvider>(
                memoryProvideWithoutConfigFile)
            .AddFeatureManagement();

        var app = builder.Build();
        app.MapOpenApi();

        app.MapGet("/", () => Results.Ok(nameof(FeatureSwitches)));

        // This will work if it's set to true in the .csproj file
        if (FeaturesFromCsProj.One)
        {
            app.MapGet("/api/first", () => Results.Ok("Enabled in .csproj"));
        }

        if (FeaturesFromCsProj.Two)
        {
            FeaturesFromCsProj.AddFeatureTwoEndpoint(app);
            FeaturesFromCsProj.TrimmedIfNotUsed();
        }

        // Get the feature manager from the services to add an API endpoint
        var fm = app.Services.GetRequiredService<IFeatureManager>();
        if (await fm.IsEnabledAsync(FeatureNames.FeatureFromManager))
        {
            app.MapGet("/api/third", () => Results.Ok($"Enabled in {nameof(FeatureManager)}"));
        }

        // Inject the feature manager to set a flag
        app.MapGet("/api/fourth", async (IFeatureManager fm) =>
        {
            var enabled = await fm.IsEnabledAsync(FeatureNames.FeatureFromManager);
            return Results.Ok($"Feature flag enabled: {enabled}");
        });

        // The OpenAPI json file will be located at http://localhost:8080/openapi/v1.json
        await app.RunAsync("http://localhost:8080");
    }
}