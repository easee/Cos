using Easee.Cos.Exceptions;
using Easee.IoT.DataTypes.Observations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Easee.Cos.Tests;

public class CosReaderTests
{
    private readonly CosReader _reader = new();

    [Fact]
    public void Decode_long_message_to_single_observation()
    {
        byte[] data = Convert.FromBase64String("AQAAAcCBYJUscAABAL17IklkIjo1MywiU3RhcnQiOiIyMDIxLTA1LTA3VDEwOjA1OjEzLjAwMFoiLCJTdG9wIjoiMjAyMS0wNS0wN1QxMjowMjo1Ni4wMDBaIiwiRW5lcmd5S3doIjoxMC4wMTI3ODgsIk1ldGVyVmFsdWVTdGFydCI6NzM5Ljk1NDczMiwiTWV0ZXJWYWx1ZVN0b3AiOjc0OS45Njc1MiwiQXV0aCI6IkQzN0JGRjdGIiwiQXV0aFJlYXNvbiI6MX0=");

        List<Observation> observations = _reader.Deserialize(data);

        Assert.Single(observations);
        Assert.True(observations.First() is Observation<string>);
    }

    [Fact]
    public void Decode_to_single_observation()
    {
        byte[] data = Convert.FromBase64String("ASAAAWCVLGMAATB4QS1gQg==");

        List<Observation> observations = _reader.Deserialize(data);

        Assert.Single(observations);
        Assert.True(observations.First() is Observation<double>);
    }

    [Fact]
    public void Decode_to_ten_observations()
    {
        byte[] data = Convert.FromBase64String("ASAAAWCVK4wACjB4AAAAADC2PffO2TC3PffO2TDKQ10t0zDLQ1y0OTDMQ1Z9sjDNQ1bcKUBtAAAABMBkAAFCQC4AAAAX");

        List<Observation> observations = _reader.Deserialize(data);

        Assert.Equal(10, observations.Count);
        Assert.True(observations[0] is Observation<double>);
        Assert.True(observations[1] is Observation<double>);
        Assert.True(observations[2] is Observation<double>);
        Assert.True(observations[3] is Observation<double>);
        Assert.True(observations[4] is Observation<double>);
        Assert.True(observations[5] is Observation<double>);
        Assert.True(observations[6] is Observation<double>);
        Assert.True(observations[7] is Observation<int>);
        Assert.True(observations[8] is Observation<string>);
        Assert.True(observations[9] is Observation<int>);
    }

    [Fact]
    public void Invalid_data_throws_exception()
    {
        byte[] data = Encoding.UTF8.GetBytes("InvalidCosData"); ;

        Assert.Throws<UnsupportedCosVersionException>(() => _reader.Deserialize(data));
    }
}
