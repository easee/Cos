using System;
using System.Collections.Generic;
using Cos.Types;
using Xunit;
using Observation = Cos.Types.Observation;

namespace Easee.Cos.Tests;

public class CircularTests
{
    private readonly CosReader _reader = new();
    private readonly CosWriter _writer = new();

    private static void AssertObservations(List<Observation> input, List<Observation> output)
    {
        Assert.Equal(input.Count, output.Count);

        for (int i = 0; i < input.Count; i++)
        {
            var obsInput = input[i];
            var obsOutput = output[i];
            Assert.Equal(0, obsInput.CompareTo(obsOutput));
        }
    }

    [Fact]
    public void Deserialize_and_serialize_multiple_observations()
    {
        string input = "AYAABBBkCNoY8rmFAAAAAQFQZQjaGPK5hQAAAAEHzCBmCNoY8rmFAAAAAT/xxqfvnbItwGcI2hjyuYUAAAABAAtoZWxsbyB3b3JsZA==";

        List<Observation> observations = _reader.Deserialize(Convert.FromBase64String(input));

        string output = Convert.ToBase64String(_writer.Serialize(observations));

        Assert.Equal(input, output);
    }

    [Fact]
    public void Serialize_and_deserialize_position()
    {
        List<Observation> input = new()
        {
            new Observation<Position>(104, DateTime.UtcNow, new Position(1, 2)),
        };

        List<Observation> output = _reader.Deserialize(_writer.Serialize(input));

        AssertObservations(input, output);
    }

    [Fact]
    public void Serialize_and_deserialize_negative_int()
    {
        List<Observation> input = new()
        {
            new Observation<int>(104, DateTime.UtcNow, -1),
        };

        List<Observation> output = _reader.Deserialize(_writer.Serialize(input));

        AssertObservations(input, output);
    }

    [Fact]
    public void Serialize_and_deserialize_position_with_altitude()
    {
        List<Observation> input = new()
        {
            new Observation<Position>(104, DateTime.UtcNow, new Position(1, 2, 3)),
        };

        List<Observation> output = _reader.Deserialize(_writer.Serialize(input));

        AssertObservations(input, output);
    }

    [Fact]
    public void Serialize_and_deserialize_multiple_observations()
    {
        List<Observation> input = new()
        {
            new Observation<bool>(100, DateTime.UtcNow, true),
            new Observation<int>(101, DateTime.UtcNow, 1996),
            new Observation<double>(102, DateTime.UtcNow, 1.111),
            new Observation<string>(103, DateTime.UtcNow, "hello world"),
            new Observation<Position>(104, DateTime.UtcNow, new Position(1, 2)),
        };

        List<Observation> output = _reader.Deserialize(_writer.Serialize(input));

        AssertObservations(input, output);
    }
}
