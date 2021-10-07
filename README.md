# COS Deserializer
COS (Compact Observation Scheme) deserializer.

This code is available as a NuGet package in Azure DevOps Artifacts NuGet feed.

For more information about the COS format, visit this repo: https://github.com/Masterloop/cos-c

## Example
```
byte[] data = <MyCosData>;
CosReader reader = new();
ListObservation> observations = reader.Deserialize(data);
```
