using System.Collections.Generic;
using Cos.Types;

namespace Easee.Cos
{
    public interface ICosReader
    {
        List<Observation> Deserialize(in byte[] cosData);
    }
}