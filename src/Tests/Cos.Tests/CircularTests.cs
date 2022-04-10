using Easee.Cos;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cos.Tests
{
    public class CircularTests
    {
        [Fact]
        public void Deserialize_and_serialize_multiple_observations()
        {
            string input = "AYAABBBkCNoY8rmFAAAAAQFQZQjaGPK5hQAAAAEHzCBmCNoY8rmFAAAAAT/xxqfvnbItwGcI2hjyuYUAAAABAAtoZWxsbyB3b3JsZA==";

            List<Observation> observations = new CosReader().Deserialize(Convert.FromBase64String(input));

            string output = CosWriter.SerializeB64(observations);

            Assert.Equal(input, output);
        }

        [Fact]
        public void Serialize_and_deserialize_position()
        {
            List<Observation> input = new()
            {
                new Observation<Position>(104, DateTime.Parse("2022-04-08"), new Position(1, 2)),
            };

            string base64 = CosWriter.SerializeB64(input);

            List<Observation> output = new CosReader().Deserialize(Convert.FromBase64String(base64));

            Assert.Equal(input.Count, output.Count);

            for (int i = 0; i < input.Count; i++)
            {
                var obsInput = input[i];
                var obsOutput = output[i];
                Assert.Equal(0, obsInput.CompareTo(obsOutput));
            }
        }

        [Fact]
        public void Serialize_and_deserialize_position_with_altitude()
        {
            List<Observation> input = new()
            {
                new Observation<Position>(104, DateTime.Parse("2022-04-08"), new Position(1, 2, 3)),
            };

            string base64 = CosWriter.SerializeB64(input);

            List<Observation> output = new CosReader().Deserialize(Convert.FromBase64String(base64));

            Assert.Equal(input.Count, output.Count);

            for (int i = 0; i < input.Count; i++)
            {
                var obsInput = input[i];
                var obsOutput = output[i];
                Assert.Equal(0, obsInput.CompareTo(obsOutput));
            }
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

            string base64 = CosWriter.SerializeB64(input);

            List<Observation> output = new CosReader().Deserialize(Convert.FromBase64String(base64));

            Assert.Equal(input.Count, output.Count);

            for (int i = 0; i < input.Count; i++)
            {
                var obsInput = input[i];
                var obsOutput = output[i];
                Assert.Equal(0, obsInput.CompareTo(obsOutput));
            }
        }
    }
}
