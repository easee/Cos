using System;

namespace Cos.Exceptions
{
    public class UnsupportedCosVersionException : Exception
    {
        public UnsupportedCosVersionException(string message) : base(message) {}
    }
}