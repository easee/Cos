using Easee.Cos;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cos.Tests
{
    public class CircularTest
    {
        [Fact]
        public void Deserialize_and_serialize_multiple_observations()
        {
            string input = "AYAABBBkCNoY8rmFAAAAAQFQZQjaGPK5hQAAAAEHzCBmCNoY8rmFAAAAAT/xxqfvnbItwGcI2hjyuYUAAAABAAtoZWxsbyB3b3JsZA==";

            List<Observation> observations = new CosReader().Deserialize(Convert.FromBase64String(input));

            string output = CosWriter.SerializeB64(observations);

            Assert.Equal(input, output);
        }
    }
}
