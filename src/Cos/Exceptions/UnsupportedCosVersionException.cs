using System;

namespace Easee.Cos.Exceptions
{
    public class UnsupportedCosVersionException : Exception
    {
        public UnsupportedCosVersionException(string message) : base(message) {}
    }
}