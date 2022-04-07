using Easee.Cos;
using System;
using Xunit;

namespace Cos.Tests
{
    public class CosWriterTests
    {
        [Fact]
        public void Write_boolean_observation()
        {
            string result = CosWriter.Serialize(new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
            });

            string expected = "AYAAARBkCNoY8rmFAAAAAQE=";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_integer_observation()
        {
            string result = CosWriter.Serialize(new()
            {
                new Observation<int>(101, DateTime.Parse("2022-04-08"), 1996),
            });

            string expected = "AYAAAVBlCNoY8rmFAAAAAQfM";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_double_observation()
        {
            string result = CosWriter.Serialize(new()
            {
                new Observation<double>(102, DateTime.Parse("2022-04-08"), 1.111),
            });

            string expected = "AYAAASBmCNoY8rmFAAAAAT/xxqfvnbIt";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_string_observation()
        {
            string result = CosWriter.Serialize(new()
            {
                new Observation<string>(103, DateTime.Parse("2022-04-08"), "hello world"),
            });

            string expected = "AYAAAcBnCNoY8rmFAAAAAQALaGVsbG8gd29ybGQ=";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_multiple_observations()
        {
            string result = CosWriter.Serialize(new()
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
