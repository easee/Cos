using System.Collections.Generic;
using Easee.Wingman.Types;

namespace Easee.Cos
{
    public interface ICosReader
    {
        List<Observation> Deserialize(in byte[] cosData);
    }
}