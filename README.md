# Feature switches / Feature flags

This is an example solution which demonstrates how to use features switches / features flags in .NET 9. The solution uses both the Microsoft approach to feature flags as well as the OpenFeature NuGet package which you can find here: [OpenFeature.dev](https://openfeature.dev).

## API URLs

- http://localhost:8080 // The API which uses the Microsoft.FeatureManagement NuGet package.
- http://localhost:8000 // The API to provide flags for the OpenFeature client.
- http://localhost:8001 // The client project which uses the custom OpenFeature API provider.
- http://localhost:8080/openapi/v1.json // OpenAPI JSON spec for the FeatureSwitches project.
- http://localhost:8001/openapi/v1.json // OpenAPI JSON spec for the OpenFeature client API.

### How to run

```sh
cd src
dotnet restore
dotnet build
dotnet run --project FeatureSwitches
dotnet run --project OpenFeatureApp
dotnet run --project OpenFeatureProvider
```

#### IL disassemblers to check compiler trimming

[AvaloniaILSpy](https://github.com/icsharpcode/AvaloniaILSpy/releases)
[IlDasm2](https://github.com/lextudio/dotnet-ildasm2)

#### MultiRun helper tool to run multiple projects at once with one command

```sh
git clone https://github.com/zacharyMcSweenManickchand/MultiRun.net.git
cd MultiRun.net
dotnet build -c Release && dotnet pack -c Release
dotnet tool install --global --add-source ./nupkg mrn --version 1.0.0
mrn .
```
