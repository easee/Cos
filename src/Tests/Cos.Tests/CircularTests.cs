using Easee.Cos;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cos.Tests
{
    public class CircularTests
    {
        private readonly CosReader _reader = new();
        private readonly CosWriter _writer = new();

        private void AssertObservations(List<Observation> input, List<Observation> output)
        {
            Assert.Equal(input.Count, output.Count);

            for (int i = 0; i < input.Count; i++)
            {
                var obsInput = input[i];
                var obsOutput = output[i];
                Assert.Equal(0, obsInput.CompareTo(obsOutput));
            }
        }

        [Fact]
        public void Deserialize_and_serialize_multiple_observations()
        {
            string input = "AYAABBBkCNoY8rmFAAAAAQFQZQjaGPK5hQAAAAEHzCBmCNoY8rmFAAAAAT/xxqfvnbItwGcI2hjyuYUAAAABAAtoZWxsbyB3b3JsZA==";

            List<Observation> observations = _reader.Deserialize(Convert.FromBase64String(input));

            string output = _writer.SerializeB64(observations);

            Assert.Equal(input, output);
        }

        [Fact]
        public void Serialize_and_deserialize_position()
        {
            List<Observation> input = new()
            {
                new Observation<Position>(104, DateTime.Parse("2022-04-08"), new Position(1, 2)),
            };

            List<Observation> output = _reader.Deserialize(_writer.SerializeCos(input));

            AssertObservations(input, output);
        }

        [Fact]
        public void Serialize_and_deserialize_position_with_altitude()
        {
            List<Observation> input = new()
            {
                new Observation<Position>(104, DateTime.Parse("2022-04-08"), new Position(1, 2, 3)),
            };

            List<Observation> output = _reader.Deserialize(_writer.SerializeCos(input));

            AssertObservations(input, output);
        }

        [Fact]
        public void Serialize_and_deserialize_multiple_observations()
        {
            List<Observation> input = new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
                new Observation<int>(101, DateTime.Parse("2022-04-08"), 1996),
                new Observation<double>(102, DateTime.Parse("2022-04-08"), 1.111),
                new Observation<string>(103, DateTime.Parse("2022-04-08"), "hello world"),
                new Observation<Position>(104, DateTime.Parse("2022-04-08"), new Position(1, 2)),
            };

            List<Observation> output = _reader.Deserialize(_writer.SerializeCos(input));

            AssertObservations(input, output);
        }
    }
}
