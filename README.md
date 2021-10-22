# COS Deserializer
COS (Compact Observation Scheme) deserializer.

This code is available as a NuGet package in [Easee's Github Package Feed](https://github.com/orgs/easee/packages)

For more information about the COS format, visit the [COS-C repo](https://github.com/Masterloop/cos-c)

## Example
```
byte[] data = <MyCosData>;
ICosReader reader = new CosReader();
List<Observation> observations = reader.Deserialize(data);
```

## Release/deploy
All builds on *main* branch with tags on the format v1.2.3 are built, tested and released as a NuGet package. NuGet requires semantic versioning and that new version numbers are greater that the previous ones. To push a new NuGet package to the feed - create a new "Release" in the Github repository and tag it with the format "vx.y.z".

Before releasing a new version, run the benchmark project in the *Tests* folder with and without the changes to see the impact on performance. Run the application from the command line, build in *Release*. This will print the benchmark results.

## Add NuGet feed
To use this package, add the NuGet feed to your environment. This can be done in several ways, depending on your environment/IDE.
The packages are [here](https://github.com/orgs/easee/packages).

NuGet feed URL: [https://nuget.pkg.github.com/easee/index.json](https://nuget.pkg.github.com/easee/index.json)

```
dotnet nuget add source --username [username] --password [Personal Access Token] --store-password-in-clear-text --name github "https://nuget.pkg.github.com/easee/index.json"
```

### Adding feed with Visual studio

Add the NuGet feed URL using the NuGet Package Manager UI (Tools -> NuGet Package Manager -> Package Manager Settings -> Package Sources).

When accessing the feed for the first time, Visual Studio will prompt you for your credentials. Use your Github username as username and a PAT as password.
Generate the PAT on the Github webpage. Go to your profile -> Developer settings -> Personal access tokens and generate a new one. Grant it access to read packages.


### Rider
Use [this guide](https://www.jetbrains.com/help/rider/Using_NuGet.html#sources)

## Consuming Nuget feed (authenticate to use feed )

If the feed already has been added and you need to authenticate:

1. Create a Personal Access Token (PAT) with the atleast the scope: `read:packages`.

2. Use your usual username and PAT as password.



