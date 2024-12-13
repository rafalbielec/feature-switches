# feature-switches

Example application which demonstrates how to use features switches / features flags in .NET 9. The solution uses both the Microsoft approach to feature flags as well as the OpenFeature NuGet package.

## IL disassemblers

[AvaloniaILSpy](https://github.com/icsharpcode/AvaloniaILSpy/releases)
[IlDasm2](https://github.com/lextudio/dotnet-ildasm2)

# OpenFeature addresses

- https://localhost:8080 // The API which uses the Microsoft.FeatureManagement NuGet package.
- http://localhost:8000 // The API to provide flags for the OpenFeature client.
- http://localhost:8001 // The client project which uses the custom ApiProvider.

