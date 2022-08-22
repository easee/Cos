using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Easee.Cos.Types;

public abstract class Observation : IEquatable<object>
{
    public Observation(int id, DateTime timestamp, ObservationType type)
    {
        Id = id;
        Type = type;
        Timestamp = timestamp;
    }

    public int Id { get; }
    public ObservationType Type { get; }
    public DateTime Timestamp { get; }

    public abstract override bool Equals(object? other);
    public abstract override int GetHashCode();

    public static bool operator ==(Observation a, Observation b) => a.Equals(b);
    public static bool operator !=(Observation a, Observation b) => !a.Equals(b);

    public override string ToString() => JsonSerializer.Serialize(this);
}

public class Observation<TValue> : Observation
{
    public Observation(int id, DateTime timestamp, TValue value)
        : base(id, timestamp, typeof(TValue) switch
        {
            Type i when i == typeof(int) => ObservationType.Integer,
            Type d when d == typeof(double) => ObservationType.Double,
            Type b when b == typeof(bool) => ObservationType.Boolean,
            Type s when s == typeof(string) => ObservationType.String,
            Type p when p == typeof(Position) => ObservationType.Position,
            _ => throw new ArgumentException($"Invalid observation type: {typeof(TValue).FullName}", nameof(value)),
        })
    {
        Value = value;
    }

    public TValue Value { get; }

    public override bool Equals(object? other)
    {
        return (other is Observation<TValue> o
                && o.Id == Id
                && o.Type == Type
                && o.Timestamp == Timestamp
                && EqualityComparer<TValue>.Default.Equals(Value, o.Value));
    }

    public override int GetHashCode()
    {
        return $"{Id}_{Type}_{Timestamp}_{Value}".GetHashCode();
    }
}