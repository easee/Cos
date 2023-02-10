using Easee.IoT.DataTypes.Observations;
using System.Collections.Generic;
using System.IO;

namespace Easee.Cos
{
    public interface ICosReader
    {
        List<Observation> Deserialize(in byte[] cosData);
        List<Observation> Deserialize(Stream stream);
    }
}