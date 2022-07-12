using Easee.Cos;
using System;
using Easee.Cos.Exceptions;
using Xunit;

namespace Cos.Tests
{
    public class CosWriterTests
    {
        private readonly CosWriter _writer = new();

        [Fact]
        public void Cos_version_0_should_throw()
        {
            var err = Assert.Throws<UnsupportedCosVersionException>(() => _writer.Serialize(new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
            }, 0));

            Assert.Equal("Unsupported COS version: 0", err.Message);
        }

        [Fact]
        public void Cos_version_too_high_should_throw()
        {
            var err = Assert.Throws<UnsupportedCosVersionException>(() => _writer.Serialize(new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
            }, 2));

            Assert.Equal("Unsupported COS version: 2", err.Message);
        }

        [Fact]
        public void Header_byte_with_multi_observations_should_throw()
        {
            var err = Assert.Throws<ArgumentException>(() => _writer.Serialize(new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
            }, 1, (byte)COSHeaderFlag.COS_HEADER_MULTI_OBSERVATIONS));

            Assert.Equal("cosHeaderFlags not supported: COS_HEADER_MULTI_OBSERVATIONS", err.Message);
        }

        [Fact]
        public void Header_byte_with_multi_timestamps_should_throw()
        {
            var err = Assert.Throws<ArgumentException>(() => _writer.Serialize(new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
            }, 1, (byte)COSHeaderFlag.COS_HEADER_MULTI_TIMESTAMPS));

            Assert.Equal("cosHeaderFlags not supported: COS_HEADER_MULTI_TIMESTAMPS", err.Message);
        }

        [Fact]
        public void Write_boolean_observation()
        {
            string result = Convert.ToBase64String(_writer.Serialize(new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
            }));

            string expected = "AYAAARBkCNoY8rmFAAAAAQE=";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_integer_observation()
        {
            string result = Convert.ToBase64String(_writer.Serialize(new()
            {
                new Observation<int>(101, DateTime.Parse("2022-04-08"), 1996),
            }));

            string expected = "AYAAAVBlCNoY8rmFAAAAAQfM";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_double_observation()
        {
            string result = Convert.ToBase64String(_writer.Serialize(new()
            {
                new Observation<double>(102, DateTime.Parse("2022-04-08"), 1.111),
            }));

            string expected = "AYAAASBmCNoY8rmFAAAAAT/xxqfvnbIt";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_string_observation()
        {
            string result = Convert.ToBase64String(_writer.Serialize(new()
            {
                new Observation<string>(103, DateTime.Parse("2022-04-08"), "hello world"),
            }));

            string expected = "AYAAAcBnCNoY8rmFAAAAAQALaGVsbG8gd29ybGQ=";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Write_multiple_observations()
        {
            string result = Convert.ToBase64String(_writer.Serialize(new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
                new Observation<int>(101, DateTime.Parse("2022-04-08"), 1996),
                new Observation<double>(102, DateTime.Parse("2022-04-08"), 1.111),
                new Observation<string>(103, DateTime.Parse("2022-04-08"), "hello world"),
            }));

            string expected = "AYAABBBkCNoY8rmFAAAAAQFQZQjaGPK5hQAAAAEHzCBmCNoY8rmFAAAAAT/xxqfvnbItwGcI2hjyuYUAAAABAAtoZWxsbyB3b3JsZA==";

            Assert.Equal(expected, result);
        }
    }
}
