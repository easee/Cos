using Easee.Cos;
using System;
using System.IO;
using Xunit;

namespace Cos.Tests
{
    public class BigEndianReaderTests
    {
        [Fact]
        public void ReadInt16()
        {
            byte[] cosData = BitConverter.GetBytes((Int16)1337);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(cosData);
            }
            using MemoryStream stream = new(cosData);
            using BigEndianBinaryReader reader = new(stream);

            short result = reader.ReadInt16();

            Assert.Equal(1337, result);
        }

        [Fact]
        public void ReadUInt16()
        {
            byte[] cosData = BitConverter.GetBytes((UInt16)1337);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(cosData);
            }
            using MemoryStream stream = new(cosData);
            using BigEndianBinaryReader reader = new(stream);

            ushort result = reader.ReadUInt16();

            Assert.Equal(1337, result);
        }

        [Fact]
        public void ReadInt32()
        {
            byte[] cosData = BitConverter.GetBytes((Int32)1337);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(cosData);
            }
            using MemoryStream stream = new(cosData);
            using BigEndianBinaryReader reader = new(stream);

            int result = reader.ReadInt32();

            Assert.Equal(1337, result);
        }

        [Fact]
        public void ReadUInt32()
        {
            byte[] cosData = BitConverter.GetBytes((UInt32)1337);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(cosData);
            }
            using MemoryStream stream = new(cosData);
            using BigEndianBinaryReader reader = new(stream);

            uint result = reader.ReadUInt32();

            Assert.Equal((UInt32)1337, result);
        }

        [Fact]
        public void ReadInt64()
        {
            byte[] cosData = BitConverter.GetBytes((Int64)1337);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(cosData);
            }
            using MemoryStream stream = new(cosData);
            using BigEndianBinaryReader reader = new(stream);

            long result = reader.ReadInt64();

            Assert.Equal(1337, result);
        }

        [Fact]
        public void ReadUInt64()
        {
            byte[] cosData = BitConverter.GetBytes((UInt64)1337);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(cosData);
            }
            using MemoryStream stream = new(cosData);
            using BigEndianBinaryReader reader = new(stream);

            ulong result = reader.ReadUInt64();

            Assert.Equal((UInt64)1337, result);
        }

        [Fact]
        public void ReadSingle()
        {
            byte[] cosData = BitConverter.GetBytes(1337.0f);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(cosData);
            }
            using MemoryStream stream = new(cosData);
            using BigEndianBinaryReader reader = new(stream);

            float data = reader.ReadSingle();

            Assert.Equal(1337.0f, data);
        }

        [Fact]
        public void ReadDouble()
        {
            byte[] cosData = BitConverter.GetBytes(1337.0);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(cosData);
            }
            using MemoryStream stream = new(cosData);
            using BigEndianBinaryReader reader = new(stream);

            double data = reader.ReadDouble();

            Assert.Equal(1337, data);
        }
    }
}
