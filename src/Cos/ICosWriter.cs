using System.Collections.Generic;

namespace Easee.Cos
{
    public interface ICosWriter
    {
        string SerializeB64(List<Observation> observations, byte version = 1, byte cosHeaderFlags = (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS);
        byte[] SerializeCos(List<Observation> observations, byte version = 1, byte cosHeaderFlags = (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS);
  }
}
