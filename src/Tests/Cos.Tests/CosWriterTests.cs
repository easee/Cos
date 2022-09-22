using Easee.Cos.Exceptions;
using Easee.IoT.DataTypes.Observations;
using System;
using Xunit;

namespace Easee.Cos.Tests;

public class CosWriterTests
{
    private static readonly CosWriter _writer = new();
    private static readonly DateTime _timestamp = new(2022, 4, 8, 0, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void Cos_version_0_should_throw()
    {
        var err = Assert.Throws<UnsupportedCosVersionException>(() => _writer.Serialize(new()
        {
            new Observation<bool>(100, _timestamp, DateTime.UnixEpoch, true),
        }, 0));

        Assert.Equal("Unsupported COS version: 0", err.Message);
    }

    [Fact]
    public void Cos_version_too_high_should_throw()
    {
        var err = Assert.Throws<UnsupportedCosVersionException>(() => _writer.Serialize(new()
        {
            new Observation<bool>(100, _timestamp, DateTime.UnixEpoch, true),
        }, 2));

        Assert.Equal("Unsupported COS version: 2", err.Message);
    }

    [Fact]
    public void Header_byte_with_multi_observations_should_throw()
    {
        var err = Assert.Throws<ArgumentException>(() => _writer.Serialize(new()
        {
            new Observation<bool>(100, _timestamp, DateTime.UnixEpoch, true),
        }, 1, (byte)COSHeaderFlag.COS_HEADER_MULTI_OBSERVATIONS));

        Assert.Equal("cosHeaderFlags not supported: COS_HEADER_MULTI_OBSERVATIONS", err.Message);
    }

    [Fact]
    public void Header_byte_with_multi_timestamps_should_throw()
    {
        var err = Assert.Throws<ArgumentException>(() => _writer.Serialize(new()
        {
            new Observation<bool>(100, _timestamp, DateTime.UnixEpoch, true),
        }, 1, (byte)COSHeaderFlag.COS_HEADER_MULTI_TIMESTAMPS));

        Assert.Equal("cosHeaderFlags not supported: COS_HEADER_MULTI_TIMESTAMPS", err.Message);
    }

    [Fact]
    public void Write_boolean_observation()
    {
        string result = Convert.ToBase64String(_writer.Serialize(new()
        {
            new Observation<bool>(100, _timestamp, DateTime.UnixEpoch, true),
        }));

        string expected = "AYAAARBkCNoY8rmFAAAAAQE=";

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Write_integer_observation()
    {
        string result = Convert.ToBase64String(_writer.Serialize(new()
        {
            new Observation<int>(101, _timestamp, DateTime.UnixEpoch, 1996),
        }));

        string expected = "AYAAAVBlCNoY8rmFAAAAAQfM";

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Write_negative_integer_observation()
    {
        string result = Convert.ToBase64String(_writer.Serialize(new()
        {
            new Observation<int>(100, _timestamp, DateTime.UnixEpoch, -1),
        }));

        string expected = "AYAAAXBkCNoY8rmFAAAAAf8=";

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Write_double_observation()
    {
        string result = Convert.ToBase64String(_writer.Serialize(new()
        {
            new Observation<double>(102, _timestamp, DateTime.UnixEpoch, 1.111),
        }));

        string expected = "AYAAASBmCNoY8rmFAAAAAT/xxqfvnbIt";

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Write_string_observation()
    {
        string result = Convert.ToBase64String(_writer.Serialize(new()
        {
            new Observation<string>(103, _timestamp, DateTime.UnixEpoch, "hello world"),
        }));

        string expected = "AYAAAcBnCNoY8rmFAAAAAQALaGVsbG8gd29ybGQ=";

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Write_multiple_observations()
    {
        string result = Convert.ToBase64String(_writer.Serialize(new()
        {
            new Observation<bool>(100, _timestamp, DateTime.UnixEpoch, true),
            new Observation<int>(101, _timestamp, DateTime.UnixEpoch, 1996),
            new Observation<double>(102, _timestamp, DateTime.UnixEpoch, 1.111),
            new Observation<string>(103, _timestamp, DateTime.UnixEpoch, "hello world"),
        }));

        string expected = "AYAABBBkCNoY8rmFAAAAAQFQZQjaGPK5hQAAAAEHzCBmCNoY8rmFAAAAAT/xxqfvnbItwGcI2hjyuYUAAAABAAtoZWxsbyB3b3JsZA==";

        Assert.Equal(expected, result);
    }
}
