using System;
using System.Collections.Generic;

namespace Easee.Cos
{
    public abstract class Observation : IComparable<Observation>
    {
        public Observation(int observationId, DateTime timestamp)
        {
            ObservationId = observationId;
            Timestamp = timestamp;
        }

        public int ObservationId { get; }
        public DateTime Timestamp { get; }

        public override string ToString() => $"ID:{ObservationId}.";

        public virtual int CompareTo(Observation? other) => throw new NotImplementedException();
    }

    public class Observation<TValue> : Observation
    {
        public Observation(int observationId, DateTime timestamp, TValue value)
            : base(observationId, timestamp)
        {
            Value = value;
        }

        public TValue Value { get; }
        public override string ToString() => $"ID:{ObservationId}. Value:{Value}. Timestamp:{Timestamp}.";

        public override int CompareTo(Observation? other) {
            return (other is Observation<TValue> o
                && o.ObservationId == ObservationId
                && o.Timestamp == Timestamp
                && Comparer<TValue>.Default.Compare(Value, o.Value) == 0) 
                ? 0 : 1;
        }
    }
}
