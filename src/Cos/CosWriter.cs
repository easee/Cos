using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace Easee.Cos
{
    /// <summary>
    /// Basic implementation of COSWriter.
    /// Note: This implementation is very basic, and is not byte efficient. It will allocate on section per observation.
    /// </summary>
    public class CosWriter
    {
        public static string Serialize(List<Observation> observations, string encoding = "cos", byte version = 1, byte cosHeaderFlags = (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS)
        {
            if ((cosHeaderFlags & (byte)COSHeaderFlag.COS_HEADER_MULTI_OBSERVATIONS) != 0)
            {
                throw new ArgumentException("cosHeaderFlags not supported: COS_HEADER_MULTI_OBSERVATIONS");
            }
            if ((cosHeaderFlags & (byte)COSHeaderFlag.COS_HEADER_MULTI_TIMESTAMPS) != 0)
            {
                throw new ArgumentException("cosHeaderFlags not supported: COS_HEADER_MULTI_TIMESTAMPS");
            }

            byte[] bytes = encoding switch
            {
                "cos" => SerializeCOS(observations, version, cosHeaderFlags),
                "b64" => SerializeCOSB64(observations, version, cosHeaderFlags),
                _ => throw new ArgumentException("Unknown encoding: " + encoding),
            };

            return Convert.ToBase64String(bytes);
        }

        private static byte[] SerializeCOSB64(List<Observation> observations, byte version, byte cosHeaderFlags)
        {
            byte[] cos = SerializeCOS(observations, version, cosHeaderFlags);

            string encodedString = Convert.ToBase64String(cos);

            return Encoding.UTF8.GetBytes(encodedString);
        }

        private static byte[] SerializeCOS(List<Observation> observations, byte version, byte cosHeaderFlags)
        {
            using MemoryStream stream = new();
            using (BigEndianBinaryWriter writer = new(new BinaryWriter(stream)))
            {
                writer.WriteByte(version);
                switch (version)
                {
                    case 1:
                        SerializeCOSVersion1(writer, observations, cosHeaderFlags);
                        break;
                    default:
                        throw new Exception($"Unsupported COS version: {version}");
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
            return obs switch
            {
                Observation<bool> => COSObservationType.COS_OBS_TYPE_BOOLEAN,
                Observation<double> => COSObservationType.COS_OBS_TYPE_DOUBLE,
                Observation<string> => COSObservationType.COS_OBS_TYPE_UTF8,
                Observation<Position> => COSObservationType.COS_OBS_TYPE_POSITION_3D,
                Observation<int> o => o switch
                {
                    { Value: >= -128 and <= 127 } => COSObservationType.COS_OBS_TYPE_INT8,
                    { Value: >= -32768 and <= 32767 } => COSObservationType.COS_OBS_TYPE_INT16,
                    _ => COSObservationType.COS_OBS_TYPE_INT32
                },
                _ => throw new ArgumentException($"Unsupported observation type"),
            };
        }

        private static void WriteObservationValue(Observation obs, BigEndianBinaryWriter writer)
        {
            switch (obs)
            {
                case Observation<bool> o:
                    writer?.WriteByte((byte)(o.Value ? 1 : 0));
                    return; ;
                case Observation<double> o:
                    writer?.WriteDouble(o.Value);
                    return;
                case Observation<string> o:
                    var utf8 = Encoding.UTF8.GetBytes(o.Value);
                    writer?.WriteUInt16((ushort)utf8.Length);
                    writer?.WriteBytes(utf8);
                    return;
                case Observation<Position> o:
                    writer?.WriteSingle((float)o.Value.Latitude);
                    writer?.WriteSingle((float)o.Value.Longitude);
                    if (o.Value.Altitude != null) writer?.WriteSingle((float)o.Value.Altitude);
                    if (o.Value.DOP != null) writer?.WriteSingle((float)o.Value.DOP);
                    return;
                case Observation<int> o:
                    switch (o)
                    {
                        case { Value: >= -128 and <= 127 }:
                            writer?.WriteChar((char)o.Value);
                            return;
                        case { Value: >= -32768 and <= 32767 }:
                            writer?.WriteInt16((short)o.Value);
                            return;
                        default:
                            writer?.WriteInt32(o.Value);
                            return;
                    }
                default:
                    throw new ArgumentException($"Unsupported observation type");
            }
        }
    }
}
