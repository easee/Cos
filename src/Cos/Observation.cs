using System;

namespace Easee.Cos
{
    public abstract class Observation
    {
        public Observation(int observationId, DateTime timestamp)
        {
            ObservationId = observationId;
            Timestamp = timestamp;
        }

        public int ObservationId { get; }
        public DateTime Timestamp { get; }

        public override string ToString()
        {
            return $"ID:{ObservationId}.";
        }
    }

    public class Observation<TValue> : Observation
    {
        public Observation(int observationId, DateTime timestamp, TValue value)
            : base(observationId, timestamp)
        {
            Value = value;
        }

        public TValue Value { get; }
        public override string ToString()
        {
            return $"ID:{ObservationId}. Value:{Value}. Timestamp:{Timestamp}.";
        }
    }
}
