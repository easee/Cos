using System;

namespace Easee.Cos.Exceptions
{
    public class UnsupportedObservationTypeException : ArgumentException
    {
        public UnsupportedObservationTypeException(string message) : base(message) {}
        public UnsupportedObservationTypeException(Type type) : base(type.FullName) { }
    }
}