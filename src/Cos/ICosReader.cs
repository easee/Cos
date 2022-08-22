using Easee.Cos.Types;
using System.Collections.Generic;

namespace Easee.Cos
{
    public interface ICosReader
    {
        List<Observation> Deserialize(in byte[] cosData);
    }
}