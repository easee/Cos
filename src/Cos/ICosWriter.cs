using System.Collections.Generic;
using Easee.Wingman.Types;

namespace Easee.Cos;

public interface ICosWriter
{
    byte[] Serialize(List<Observation> observations, byte version = 1, byte cosHeaderFlags = (byte)COSHeaderFlag.COS_HEADER_64BIT_TIMESTAMPS);
}
