using System;
using System.IO;

namespace Easee.Cos
{
    public class BigEndianBinaryWriter : IDisposable
    {
        private readonly BinaryWriter _baseWriter;

        public BigEndianBinaryWriter(BinaryWriter baseWriter)
        {
            _baseWriter = baseWriter;
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Close();
            }

            _disposed = true;
        }

        ~BigEndianBinaryWriter()
        {
            Dispose(false);
        }

        public void WriteByte(byte value)
        {
            _baseWriter.Write(value);
        }

        public void WriteChar(char value)
        {
            _baseWriter.Write(value);
        }

        public void WriteBytes(byte[] bytes)
        {
            _baseWriter.Write(bytes);
        }

        public void WriteInt16(Int16 value)
        {
            WriteBigEndianBytes(BitConverter.GetBytes(value));
        }

        public void WriteUInt16(UInt16 value)
        {
            WriteBigEndianBytes(BitConverter.GetBytes(value));
        }

        public void WriteInt32(Int32 value)
        {
            WriteBigEndianBytes(BitConverter.GetBytes(value));
        }

        public void WriteUInt32(UInt32 value)
        {
            WriteBigEndianBytes(BitConverter.GetBytes(value));
        }

        public void WriteInt64(Int64 value)
        {
            WriteBigEndianBytes(BitConverter.GetBytes(value));
        }

        public void WriteSingle(Single value)
        {
            WriteBigEndianBytes(BitConverter.GetBytes(value));
        }

        public void WriteDouble(Double value)
        {
            WriteBigEndianBytes(BitConverter.GetBytes(value));
        }

        private void WriteBigEndianBytes(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            _baseWriter.Write(bytes);
        }

        private void Close()
        {
            _baseWriter.Close();
        }
    }
}
