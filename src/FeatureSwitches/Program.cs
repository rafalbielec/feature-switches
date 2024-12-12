
namespace FeatureSwitches;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
        });

        var app = builder.Build();

        app.MapGet("/", () => Results.Ok(nameof(FeatureSwitches)));

        await app.RunAsync();
    }
}