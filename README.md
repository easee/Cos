# COS Deserializer
COS (Compact Observation Scheme) deserializer.

This code is available as a NuGet package in [Azure DevOps Artifacts NuGet feed](https://dev.azure.com/easee-norway/easee-pipelines/_packaging?_a=feed&feed=easee-norway%40Local)

For more information about the COS format, visit the [COS-C repo](https://github.com/Masterloop/cos-c)

## Example
```
byte[] data = <MyCosData>;
ICosReader reader = new CosReader();
ListObservation> observations = reader.Deserialize(data);
```
