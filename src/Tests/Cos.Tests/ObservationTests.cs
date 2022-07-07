using Easee.Cos;
using Easee.Cos.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cos.Tests
{
    public class ObservationTests
    {
        [Fact]
        public void Boolean_type()
        {
            var observation = new Observation<bool>(150, DateTime.UtcNow, true);
            Assert.Equal(ObservationType.Boolean, observation.Type);
        }

        [Fact]
        public void Double_type()
        {
            var observation = new Observation<double>(150, DateTime.UtcNow, 1.234);
            Assert.Equal(ObservationType.Double, observation.Type);
        }

        [Fact]
        public void Integer_type()
        {
            var observation = new Observation<int>(150, DateTime.UtcNow, 12);
            Assert.Equal(ObservationType.Integer, observation.Type);
        }

        [Fact]
        public void String_type()
        {
            var observation = new Observation<string>(150, DateTime.UtcNow, "hello world");
            Assert.Equal(ObservationType.String, observation.Type);
        }

        [Fact]
        public void Position_type()
        {
            var observation = new Observation<Position>(150, DateTime.UtcNow, new(59, 10));
            Assert.Equal(ObservationType.Position, observation.Type);
        }

        [Fact]
        public void Unsupported_type()
        {
            var err = Assert.Throws<UnsupportedObservationTypeException>(() =>
            {
                new Observation<List<string>>(150, DateTime.UtcNow, new() { });
            });

            Assert.Equal(typeof(List<string>).FullName, err.Message);
        }
    }
}
