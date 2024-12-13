namespace OpenFeatureProvider;

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

        var app = builder.Build();
        app.MapGet("/", () => string.Empty);

        app.MapGet("/flags/bool/{name}", (string name) =>
        {
            Console.WriteLine($"Requested the {name} flag.");
            return Results.Ok(true);
        });

        await app.RunAsync("http://localhost:8000");
    }
}