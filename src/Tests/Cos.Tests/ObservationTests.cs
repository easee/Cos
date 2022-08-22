using Easee.Cos.Types;
using System;
using Xunit;

namespace Easee.Cos.Tests.Types;

public class ObservationTests
{
    [Fact]
    public void Boolean_type()
    {
        var observation = new Observation<bool>(150, DateTime.UtcNow, true);
        Assert.Equal(ObservationType.Boolean, observation.Type);
    }

    [Fact]
    public void Double_type()
    {
        var observation = new Observation<double>(150, DateTime.UtcNow, 1.234);
        Assert.Equal(ObservationType.Double, observation.Type);
    }

    [Fact]
    public void Integer_type()
    {
        var observation = new Observation<int>(150, DateTime.UtcNow, 12);
        Assert.Equal(ObservationType.Integer, observation.Type);
    }

    [Fact]
    public void String_type()
    {
        var observation = new Observation<string>(150, DateTime.UtcNow, "hello world");
        Assert.Equal(ObservationType.String, observation.Type);
    }

    [Fact]
    public void Position_type()
    {
        var observation = new Observation<Position>(150, DateTime.UtcNow, new(59, 10));
        Assert.Equal(ObservationType.Position, observation.Type);
    }

    [Fact]
    public void Unsupported_type()
    {
        var err = Assert.Throws<ArgumentException>(() =>
        {
            new Observation<UInt16>(150, DateTime.UtcNow, new() { });
        });

        Assert.Equal("Invalid observation type: System.UInt16 (Parameter 'value')", err.Message);
    }

    [Fact]
    public void Equality_operator_should_return_true_when_values_are_equal()
    {
        Observation a = new Observation<bool>(150, new DateTime(2022, 08, 22), true);
        Observation b = new Observation<bool>(150, new DateTime(2022, 08, 22), true);
        Assert.True(a == b);
        Assert.False(a != b);

        a = new Observation<string>(150, new DateTime(2022, 08, 22), "hello world");
        b = new Observation<string>(150, new DateTime(2022, 08, 22), "hello world");
        Assert.True(a == b);
        Assert.False(a != b);

        a = new Observation<int>(150, new DateTime(2022, 08, 22), 10);
        b = new Observation<int>(150, new DateTime(2022, 08, 22), 10);
        Assert.True(a == b);
        Assert.False(a != b);

        a = new Observation<double>(150, new DateTime(2022, 08, 22), 1.234);
        b = new Observation<double>(150, new DateTime(2022, 08, 22), 1.234);
        Assert.True(a == b);
        Assert.False(a != b);

        a = new Observation<Position>(150, new DateTime(2022, 08, 22), new(59, 10));
        b = new Observation<Position>(150, new DateTime(2022, 08, 22), new(59, 10));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact]
    public void Equality_operator_should_return_false_when_values_are_different()
    {
        Observation a = new Observation<bool>(150, new DateTime(2022, 08, 22), true);
        Observation b = new Observation<bool>(151, new DateTime(2022, 08, 22), true);
        Assert.False(a == b);
        Assert.True(a != b);

        a = new Observation<bool>(150, new DateTime(2022, 08, 22), true);
        b = new Observation<bool>(150, new DateTime(2022, 08, 23), true);
        Assert.False(a == b);
        Assert.True(a != b);

        a = new Observation<bool>(150, new DateTime(2022, 08, 22), true);
        b = new Observation<bool>(150, new DateTime(2022, 08, 22), false);
        Assert.False(a == b);
        Assert.True(a != b);

        a = new Observation<string>(150, new DateTime(2022, 08, 22), "hello world");
        b = new Observation<string>(150, new DateTime(2022, 08, 22), "hello \0world");
        Assert.False(a == b);
        Assert.True(a != b);

        a = new Observation<int>(150, new DateTime(2022, 08, 22), 10);
        b = new Observation<int>(150, new DateTime(2022, 08, 22), 11);
        Assert.False(a == b);
        Assert.True(a != b);

        a = new Observation<double>(150, new DateTime(2022, 08, 22), 1.234);
        b = new Observation<double>(150, new DateTime(2022, 08, 22), 2.345);
        Assert.False(a == b);
        Assert.True(a != b);

        a = new Observation<Position>(150, new DateTime(2022, 08, 22), new(59, 10));
        b = new Observation<Position>(150, new DateTime(2022, 08, 22), new(59, 11));
        Assert.False(a == b);
        Assert.True(a != b);
    }
}