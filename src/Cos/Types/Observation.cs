using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Easee.Cos.Types;

public abstract class Observation : IComparable<Observation>
{
    public Observation(int observationId, DateTime timestamp, ObservationType type)
    {
        ObservationId = observationId;
        Type = type;
        Timestamp = timestamp;
    }

    public int ObservationId { get; }
    public ObservationType Type { get; }
    public DateTime Timestamp { get; }

    public abstract int CompareTo(Observation? other);

    public override string ToString() => JsonSerializer.Serialize(this);
}

public class Observation<TValue> : Observation
{
    public Observation(int observationId, DateTime timestamp, TValue value)
        : base(observationId, timestamp, typeof(TValue) switch
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

    public override int CompareTo(Observation? other)
    {
        return (other is Observation<TValue> o
                && o.ObservationId == ObservationId
                && o.Type == Type
                && o.Timestamp == Timestamp
                && Comparer<TValue>.Default.Compare(Value, o.Value) == 0)
            ? 0 : 1;
    }
}