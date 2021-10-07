﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Easee.Cos
{
    /// <summary>
    /// This code originates from the Masterloop code base
    /// </summary>
    public class CosReader : ICosReader
    {
        public List<Observation> Deserialize(in byte[] cosData)
        {
            using MemoryStream stream = new(cosData);
            using BigEndianBinaryReader reader = new(stream);

            byte cosVersion = reader.ReadByte();
            if (cosVersion == 1)
            {
                return DeserializeCOSVersion1(reader);
            }
            else
            {
                throw new Exception($"Unsupported COS version: {cosVersion}");
            }
        }

        private static List<Observation> DeserializeCOSVersion1(BigEndianBinaryReader reader)
        {
            List<Observation> _observations = new();

            byte flags = reader.ReadByte();
            ushort sectionCount = reader.ReadUInt16();

            // Read sections
            for (ushort i = 0; i < sectionCount; i++)
            {
                COSObservationType observationType = COSObservationType.COS_OBS_TYPE_UNDEFINED;
                ushort observationId = 0;
                if ((flags & (byte)COSHeaderFlag.COS_HEADER_MULTI_OBSERVATIONS) == 0)
                {
                    ushort descriptor = reader.ReadUInt16();
                    Tuple<COSObservationType, ushort> sd = DecodeObservationDescriptor(descriptor);
                    observationType = sd.Item1;
                    observationId = sd.Item2;
                }
                DateTime sectionTimestamp;
                if ((flags & (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS) != 0)
                {
                    Int64 t64 = reader.ReadInt64();
                    sectionTimestamp = new DateTime(t64, DateTimeKind.Utc);
                }
                else
                {
                    UInt32 unixtime = reader.ReadUInt32();
                    sectionTimestamp = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixtime);
                }
                ushort observationCount = reader.ReadUInt16();

                // Read observations
                for (ushort j = 0; j < observationCount; j++)
                {
                    if ((flags & (byte)COSHeaderFlag.COS_HEADER_MULTI_OBSERVATIONS) != 0)
                    {
                        ushort descriptor = reader.ReadUInt16();
                        Tuple<COSObservationType, ushort> sd = DecodeObservationDescriptor(descriptor);
                        observationType = sd.Item1;
                        observationId = sd.Item2;
                    }
                    DateTime observationTimestamp;
                    if ((flags & (byte)COSHeaderFlag.COS_HEADER_MULTI_TIMESTAMPS) != 0)
                    {
                        if ((flags & (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS) != 0)
                        {
                            Int64 deltaTicks = reader.ReadInt64();
                            observationTimestamp = new DateTime(sectionTimestamp.Ticks + deltaTicks, DateTimeKind.Utc);
                        }
                        else
                        {
                            ushort deltaSections = reader.ReadUInt16();
                            observationTimestamp = sectionTimestamp.AddSeconds(deltaSections);
                        }
                    }
                    else
                    {
                        observationTimestamp = sectionTimestamp;
                    }
                    Observation observation = DecodeObservationValue(reader, observationId, observationType, observationTimestamp);
                    _observations.Add(observation);
                }
            }

            return _observations;
        }

        private static Tuple<COSObservationType, ushort> DecodeObservationDescriptor(ushort descriptor)
        {
            COSObservationType type = (COSObservationType)(descriptor >> 12);
            ushort id = (ushort)(descriptor & 0xFFF);
            return new Tuple<COSObservationType, ushort>(type, id);
        }

        private static Observation DecodeObservationValue(BigEndianBinaryReader reader, int observationId, COSObservationType observationType, DateTime timestamp)
        {
            switch (observationType)
            {
                case COSObservationType.COS_OBS_TYPE_BOOLEAN:
                    byte vBoolean = reader.ReadByte();
                    return new Observation<bool>(observationId, timestamp, (vBoolean == 1));
                case COSObservationType.COS_OBS_TYPE_DOUBLE:
                    double vDouble = reader.ReadDouble();
                    return new Observation<double>(observationId, timestamp, vDouble);
                case COSObservationType.COS_OBS_TYPE_FLOAT:
                    double vFloat = reader.ReadSingle();
                    return new Observation<double>(observationId, timestamp, vFloat);
                case COSObservationType.COS_OBS_TYPE_INT32:
                    Int32 vInt32 = reader.ReadInt32();
                    return new Observation<int>(observationId, timestamp, vInt32);
                case COSObservationType.COS_OBS_TYPE_INT16:
                    Int16 vInt16 = reader.ReadInt16();
                    return new Observation<int>(observationId, timestamp, vInt16);
                case COSObservationType.COS_OBS_TYPE_UINT16:
                    UInt16 vUInt16 = reader.ReadUInt16();
                    return new Observation<int>(observationId, timestamp, vUInt16);
                case COSObservationType.COS_OBS_TYPE_INT8:
                    sbyte vInt8 = reader.ReadSByte();
                    return new Observation<int>(observationId, timestamp, vInt8);
                case COSObservationType.COS_OBS_TYPE_UINT8:
                    byte vUInt8 = reader.ReadByte();
                    return new Observation<int>(observationId, timestamp, vUInt8);
                case COSObservationType.COS_OBS_TYPE_POSITION_2D:
                    double vPos2DLat = reader.ReadSingle();
                    double vPos2DLon = reader.ReadSingle();
                    return new Observation<Position>(observationId, timestamp, new Position(vPos2DLat, vPos2DLon));
                case COSObservationType.COS_OBS_TYPE_POSITION_3D:
                    double vPos3DLat = reader.ReadSingle();
                    double vPos3DLon = reader.ReadSingle();
                    double vPos3DAlt = reader.ReadSingle();
                    return new Observation<Position>(observationId, timestamp, new Position(vPos3DLat, vPos3DLon, vPos3DAlt));
                case COSObservationType.COS_OBS_TYPE_ASCII:
                    ushort vAsciiLength = reader.ReadUInt16();
                    char[] vAscii = (char[])reader.ReadChars(vAsciiLength);
                    return new Observation<string>(observationId, timestamp, new string(vAscii));
                case COSObservationType.COS_OBS_TYPE_UTF8:
                    ushort vUTF8Length = reader.ReadUInt16();
                    byte[] vUTF8 = reader.ReadBytes(vUTF8Length);
                    return new Observation<string>(observationId, timestamp, Encoding.UTF8.GetString(vUTF8));
                default:
                    throw new ArgumentException($"Unsupported observation type value: {observationType}.");
            }
        }

        private enum COSHeaderFlag : byte
        {
            COS_HEADER_MULTI_OBSERVATIONS = 0x20,
            COS_HEADER_MULTI_TIMESTAMPS = 0x40,
            COS_HEADER_64BIT_TIMESTAMPS = 0x80
        }

        private enum COSObservationType : byte
        {
            COS_OBS_TYPE_UNDEFINED = 0,
            COS_OBS_TYPE_BOOLEAN = 1,
            COS_OBS_TYPE_DOUBLE = 2,
            COS_OBS_TYPE_FLOAT = 3,
            COS_OBS_TYPE_INT32 = 4,
            COS_OBS_TYPE_INT16 = 5,
            COS_OBS_TYPE_UINT16 = 6,
            COS_OBS_TYPE_INT8 = 7,
            COS_OBS_TYPE_UINT8 = 8,
            COS_OBS_TYPE_POSITION_2D = 9,
            COS_OBS_TYPE_POSITION_3D = 10,
            COS_OBS_TYPE_ASCII = 11,
            COS_OBS_TYPE_UTF8 = 12
        }
    }
}
