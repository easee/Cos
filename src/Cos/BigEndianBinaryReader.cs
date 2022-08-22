using System;
using System.IO;

namespace Easee.Cos
{
    public class BigEndianBinaryReader : BinaryReader
    {
        public BigEndianBinaryReader(Stream stream)
            : base(stream)
        {
        }

        public override Int16 ReadInt16()
        {
            return BitConverter.ToInt16(ReadBigEndianBytes(2), 0);
        }

        public override UInt16 ReadUInt16()
        {
            return BitConverter.ToUInt16(ReadBigEndianBytes(2), 0);
        }

        public override Int32 ReadInt32()
        {
            return BitConverter.ToInt32(ReadBigEndianBytes(4), 0);
        }

        public override UInt32 ReadUInt32()
        {
            return BitConverter.ToUInt32(ReadBigEndianBytes(4), 0);
        }

        public override Int64 ReadInt64()
        {
            return BitConverter.ToInt64(ReadBigEndianBytes(8), 0);
        }

        public override UInt64 ReadUInt64()
        {
            return BitConverter.ToUInt64(ReadBigEndianBytes(8), 0);
        }

        public override float ReadSingle()
        {
            return BitConverter.ToSingle(ReadBigEndianBytes(4), 0);
        }

        public override Double ReadDouble()
        {
            return BitConverter.ToDouble(ReadBigEndianBytes(8), 0);
        }

        private byte[] ReadBigEndianBytes(int count)
        {
            byte[] bytes = ReadBytes(count);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }
    }
}
