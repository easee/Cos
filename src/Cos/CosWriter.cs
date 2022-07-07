using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Easee.Cos.Exceptions;

namespace Easee.Cos
{
    /// <summary>
    /// Basic implementation of COSWriter.
    /// Note: This implementation is very basic and is not byte efficient. It is only meant to be used by unit tests.
    /// </summary>
    public class CosWriter : ICosWriter
    {
        public byte[] Serialize(List<Observation> observations, byte version = 1, byte cosHeaderFlags = (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS)
        {
            if ((cosHeaderFlags & (byte)COSHeaderFlag.COS_HEADER_MULTI_OBSERVATIONS) != 0)
            {
                throw new ArgumentException("cosHeaderFlags not supported: COS_HEADER_MULTI_OBSERVATIONS");
            }
            if ((cosHeaderFlags & (byte)COSHeaderFlag.COS_HEADER_MULTI_TIMESTAMPS) != 0)
            {
                throw new ArgumentException("cosHeaderFlags not supported: COS_HEADER_MULTI_TIMESTAMPS");
            }

            using MemoryStream stream = new MemoryStream();
            using (BigEndianBinaryWriter writer = new BigEndianBinaryWriter(new BinaryWriter(stream)))
            {
                writer.WriteByte(version);
                switch (version)
                {
                    case 1:
                        SerializeCOSVersion1(writer, observations, cosHeaderFlags);
                        break;
                    default:
                        throw new UnsupportedCosVersionException($"Unsupported COS version: {version}");
                }
            }

            return stream.ToArray();
        }

        private static void SerializeCOSVersion1(BigEndianBinaryWriter writer, List<Observation> observations, byte cosHeaderFlags)
        {
            writer.WriteByte(cosHeaderFlags);

            ushort sectionCount = (ushort)observations.Count;
            writer.WriteUInt16(sectionCount);

            foreach (Observation obs in observations)
            {
                ushort descriptor = (ushort)((ushort)(GetObservationType(obs)) << 12 | ((ushort)obs.ObservationId) & 0xfff);

                writer.WriteUInt16(descriptor);

                if ((cosHeaderFlags & (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS) != 0)
                {
                    writer.WriteInt64(obs.Timestamp.Ticks);
                }
                else
                {
                    TimeSpan unixTime = obs.Timestamp - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    writer.WriteUInt32((UInt32)unixTime.TotalSeconds);
                }

                writer.WriteUInt16(1);

                WriteObservationValue(obs, writer);
            }
        }

        private static COSObservationType GetObservationType(Observation obs)
        {
            if (obs is Observation<bool>) return COSObservationType.COS_OBS_TYPE_BOOLEAN;
            if (obs is Observation<double>) return COSObservationType.COS_OBS_TYPE_DOUBLE;
            if (obs is Observation<string>) return COSObservationType.COS_OBS_TYPE_UTF8;
            if (obs is Observation<Position> obsPos)
            {
                return obsPos.Value.Altitude == null
                        ? COSObservationType.COS_OBS_TYPE_POSITION_2D
                        : COSObservationType.COS_OBS_TYPE_POSITION_3D;
            }
            if (obs is Observation<int> obsInt)
            {
                if (obsInt.Value >= -128 && obsInt.Value <= 127) return COSObservationType.COS_OBS_TYPE_INT8;
                if (obsInt.Value >= -32768 && obsInt.Value <= 32767) return COSObservationType.COS_OBS_TYPE_INT16;
                return COSObservationType.COS_OBS_TYPE_INT32;
            }

            throw new UnsupportedObservationTypeException($"Unsupported observation type");
        }

        private static void WriteObservationValue(Observation obs, BigEndianBinaryWriter writer)
        {
            switch (obs)
            {
                case Observation<bool> o:
                    writer.WriteByte((byte)(o.Value ? 1 : 0));
                    return; ;
                case Observation<double> o:
                    writer.WriteDouble(o.Value);
                    return;
                case Observation<string> o:
                    var utf8 = Encoding.UTF8.GetBytes(o.Value);
                    writer.WriteUInt16((ushort)utf8.Length);
                    writer.WriteBytes(utf8);
                    return;
                case Observation<Position> o:
                    writer.WriteSingle((float)o.Value.Latitude);
                    writer.WriteSingle((float)o.Value.Longitude);
                    if (o.Value.Altitude != null) writer.WriteSingle((float)o.Value.Altitude);
                    if (o.Value.DOP != null) writer.WriteSingle((float)o.Value.DOP);
                    return;
                case Observation<int> o:
                    if (o.Value >= -128 && o.Value <= 127)
                    {
                        writer.WriteChar((char)o.Value);
                        return;
                    }
                    if (o.Value >= -32768 && o.Value <= 32767)
                    {
                        writer.WriteInt16((short)o.Value);
                        return;
                    }
                    writer.WriteInt32(o.Value);
                    return;
                default:
                    throw new UnsupportedObservationTypeException($"Unsupported observation type");
            }
        }
    }
}
