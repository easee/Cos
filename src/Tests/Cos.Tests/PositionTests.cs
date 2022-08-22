using Easee.Cos.Types;
using Xunit;

namespace Easee.Cos.Tests.Types;

public class PositionTests
{
    [Fact]
    public void Equality_operator_should_return_true_when_values_are_equal()
    {
        Position a = new(59, 10);
        Position b = new(59, 10);
        Assert.True(a == b);
        Assert.False(a != b);

        a = new(59, 10, 1);
        b = new(59, 10, 1);
        Assert.True(a == b);
        Assert.False(a != b);

        a = new(59, 10, 1, 2);
        b = new(59, 10, 1, 2);
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact]
    public void Equality_operator_should_return_false_when_values_are_different()
    {
        Position a = new(59, 10);
        Position b = new(59, 11);
        Assert.False(a == b);
        Assert.True(a != b);

        a = new(59, 10);
        b = new(60, 10);
        Assert.False(a == b);
        Assert.True(a != b);

        a = new(59, 10, 1);
        b = new(59, 10, 2);
        Assert.False(a == b);
        Assert.True(a != b);

        a = new(59, 10, 1, 2);
        b = new(59, 10, 1, 3);
        Assert.False(a == b);
        Assert.True(a != b);
    }
}