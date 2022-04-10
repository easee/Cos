using Easee.Cos;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cos.Tests
{
    public class CosWriterTests
    {
        [Fact]
        public void Base64_result_equals_cos_result()
        {
            List<Observation> observations = new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
                new Observation<int>(101, DateTime.Parse("2022-04-08"), 1996),
                new Observation<double>(102, DateTime.Parse("2022-04-08"), 1.111),
                new Observation<string>(103, DateTime.Parse("2022-04-08"), "hello world"),
                new Observation<Position>(104, DateTime.Parse("2022-04-08"), new Position(1, 2)),
            };

            string resultCos = Convert.ToBase64String(CosWriter.Serialize(observations));
            string resultB64 = CosWriter.SerializeB64(observations);

            Assert.Equal(resultCos, resultB64);
        }

        [Fact]
        public void Write_boolean_observation()
        {
            string result = CosWriter.SerializeB64(new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
            });

            string expected = "AYAAARBkCNoY8rmFAAAAAQE=";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_integer_observation()
        {
            string result = CosWriter.SerializeB64(new()
            {
                new Observation<int>(101, DateTime.Parse("2022-04-08"), 1996),
            });

            string expected = "AYAAAVBlCNoY8rmFAAAAAQfM";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_double_observation()
        {
            string result = CosWriter.SerializeB64(new()
            {
                new Observation<double>(102, DateTime.Parse("2022-04-08"), 1.111),
            });

            string expected = "AYAAASBmCNoY8rmFAAAAAT/xxqfvnbIt";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_string_observation()
        {
            string result = CosWriter.SerializeB64(new()
            {
                new Observation<string>(103, DateTime.Parse("2022-04-08"), "hello world"),
            });

            string expected = "AYAAAcBnCNoY8rmFAAAAAQALaGVsbG8gd29ybGQ=";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_multiple_observations()
        {
            string result = CosWriter.SerializeB64(new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
                new Observation<int>(101, DateTime.Parse("2022-04-08"), 1996),
                new Observation<double>(102, DateTime.Parse("2022-04-08"), 1.111),
                new Observation<string>(103, DateTime.Parse("2022-04-08"), "hello world"),
            });

            string expected = "AYAABBBkCNoY8rmFAAAAAQFQZQjaGPK5hQAAAAEHzCBmCNoY8rmFAAAAAT/xxqfvnbItwGcI2hjyuYUAAAABAAtoZWxsbyB3b3JsZA==";

            Assert.Equal(expected, result);
        }
    }
}
