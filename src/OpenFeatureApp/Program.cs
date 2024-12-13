using OpenFeature;
using OpenFeature.Providers.Memory;

namespace OpenFeatureApp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "yyyy-MM-dd HH:mm ";
        });

        builder.Services.AddOpenApi();

        // Register your custom feature flag API provider
        await Api.Instance.SetProviderAsync(new ApiProvider("http://localhost:8000"));

        // Create a new client
        FeatureClient client = Api.Instance.GetClient();

        // Evaluate your feature flag
        var v2Enabled = await client.GetBooleanValueAsync("v2_enabled", false);

        var app = builder.Build();

        app.MapGet("/", () => nameof(OpenFeatureApp));
        app.MapGet("/v1/hello", () => nameof(OpenFeatureApp));

        if (v2Enabled)
        {
            app.MapGet("/v2/hello", () => nameof(OpenFeatureApp));
        }

        app.MapGet("/v1/example", async () =>
        {
            if (await client.GetBooleanValueAsync("example", false))
            {
                return Results.Ok("Flag enabled");
            }
            return Results.Ok("Flag Disabled");
        });

        app.MapOpenApi();

        // The OpenAPI json file will be located at http://localhost:8080/openapi/v1.json
        await app.RunAsync("http://localhost:8001");
    }

}