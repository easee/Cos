
namespace Easee.Cos
{

    public enum COSHeaderFlag : byte
    {
        COS_HEADER_MULTI_OBSERVATIONS = 32, // 0x20
        COS_HEADER_MULTI_TIMESTAMPS = 64, // 0x40
        COS_HEADER_64BIT_TIMESTAMPS = 128, // 0x80
    }

    public enum COSObservationType : byte
    {
        COS_OBS_TYPE_UNDEFINED,
        COS_OBS_TYPE_BOOLEAN,
        COS_OBS_TYPE_DOUBLE,
        COS_OBS_TYPE_FLOAT,
        COS_OBS_TYPE_INT32,
        COS_OBS_TYPE_INT16,
        COS_OBS_TYPE_UINT16,
        COS_OBS_TYPE_INT8,
        COS_OBS_TYPE_UINT8,
        COS_OBS_TYPE_POSITION_2D,
        COS_OBS_TYPE_POSITION_3D,
        COS_OBS_TYPE_ASCII,
        COS_OBS_TYPE_UTF8,
    }
}
