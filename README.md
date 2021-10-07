# COS Deserializer
COS (Compact Observation Scheme) deserializer.

This code is available as a NuGet package in [Azure DevOps Artifacts NuGet feed](https://dev.azure.com/easee-norway/easee-pipelines/_packaging?_a=feed&feed=easee-norway%40Local)

For more information about the COS format, visit the [COS-C repo](https://github.com/Masterloop/cos-c)

## Example
```
byte[] data = <MyCosData>;
ICosReader reader = new CosReader();
List<Observation> observations = reader.Deserialize(data);
```

## Add NuGet feed
To use this package, add the NuGet feed to your environment. This can be done in several ways, depending on your environment/IDE.
The feed URL is found [here](https://dev.azure.com/easee-norway/easee-pipelines/_packaging?_a=feed&feed=easee-norway%40Local) under "Connect to feed".

### NuGet
```
nuget sources Add -Name "EaseeFeed" -Source <FEED_URL>
```

### Visual Studio
Use [this guide](https://docs.microsoft.com/en-us/azure/devops/artifacts/nuget/consume?view=azure-devops&tabs=windows)

### Rider
Use [this guide](https://www.jetbrains.com/help/rider/Using_NuGet.html#sources)

## Release/deploy
All builds on *main* branch is built, tested and released as a NuGet package. NuGet requires semantic versioning and that new version numbers are greater that the previous ones. Bump the *Version* property in *Cos.csproj* before merging to *main*.