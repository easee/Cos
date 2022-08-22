using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Easee.Cos.Types;

public class Position : IEquatable<object>
{
    private static readonly JsonSerializerOptions _jsonSerializationOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public Position(double latitude, double longitude, double? altitude = null, double? dop = null)
    {
        Latitude = latitude;
        Longitude = longitude;
        Altitude = altitude;
        DOP = dop;
    }

    public double Latitude { get; }
    public double Longitude { get; }
    public double? Altitude { get; }
    public double? DOP { get; }

    public override string ToString() => JsonSerializer.Serialize(this, _jsonSerializationOptions);

    public override bool Equals(object? obj)
    {
        return (obj is Position p
                && p.Latitude == Latitude
                && p.Longitude == Longitude
                && p.Altitude == Altitude
                && p.DOP == DOP);
    }
    public override int GetHashCode() => $"{Latitude}_{Longitude}_{Altitude}_{DOP}".GetHashCode();

    public static bool operator ==(Position a, Position b) => a.Equals(b);
    public static bool operator !=(Position a, Position b) => !a.Equals(b);
}