using System;

namespace Easee.Cos.Exceptions
{
    public class UnsupportedObservationTypeException : ArgumentException
    {
        public UnsupportedObservationTypeException(string message) : base(message) {}
    }
}