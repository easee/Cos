using Easee.Cos;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cos.Tests
{
    public class CosWriterTests
    {
        [Fact]
        public void Write_boolean_observation()
        {
            List<Observation> observations = new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
            };

            byte[] cosPackage = CosWriter.Serialize(observations, "cos", 1, (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS);

            string actual = Convert.ToBase64String(cosPackage);
            string expected = "AYAAARBkCNoY8rmFAAAAAQE=";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Write_integer_observation()
        {
            List<Observation> observations = new()
            {
                new Observation<int>(101, DateTime.Parse("2022-04-08"), 1996),
            };

            byte[] cosPackage = CosWriter.Serialize(observations, "cos", 1, (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS);

            string actual = Convert.ToBase64String(cosPackage);
            string expected = "AYAAAVBlCNoY8rmFAAAAAQfM";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Write_double_observation()
        {
            List<Observation> observations = new()
            {
                new Observation<double>(102, DateTime.Parse("2022-04-08"), 1.111),
            };

            byte[] cosPackage = CosWriter.Serialize(observations, "cos", 1, (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS);

            string actual = Convert.ToBase64String(cosPackage);
            string expected = "AYAAASBmCNoY8rmFAAAAAT/xxqfvnbIt";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Write_string_observation()
        {
            List<Observation> observations = new()
            {
                new Observation<string>(103, DateTime.Parse("2022-04-08"), "hello world"),
            };

            byte[] cosPackage = CosWriter.Serialize(observations, "cos", 1, (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS);

            string actual = Convert.ToBase64String(cosPackage);
            string expected = "AYAAAcBnCNoY8rmFAAAAAQALaGVsbG8gd29ybGQ=";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Write_multiple_observations()
        {
            List<Observation> observations = new()
            {
                new Observation<bool>(100, DateTime.Parse("2022-04-08"), true),
                new Observation<int>(101, DateTime.Parse("2022-04-08"), 1996),
                new Observation<double>(102, DateTime.Parse("2022-04-08"), 1.111),
                new Observation<string>(103, DateTime.Parse("2022-04-08"), "hello world"),
            };

            byte[] cosPackage = CosWriter.Serialize(observations, "cos", 1, (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS);

            string actual = Convert.ToBase64String(cosPackage);
            string expected = "AYAABBBkCNoY8rmFAAAAAQFQZQjaGPK5hQAAAAEHzCBmCNoY8rmFAAAAAT/xxqfvnbItwGcI2hjyuYUAAAABAAtoZWxsbyB3b3JsZA==";

            Assert.Equal(expected, actual);
        }
    }
}
