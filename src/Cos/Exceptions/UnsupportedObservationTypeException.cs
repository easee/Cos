using System;

namespace Cos.Exceptions
{
    public class UnsupportedObservationTypeException : ArgumentException
    {
        public UnsupportedObservationTypeException(string message) : base(message) {}
    }
}