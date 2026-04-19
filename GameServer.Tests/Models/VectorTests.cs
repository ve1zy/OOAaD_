using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Models;

public class VectorTests
{
    [Fact]
    public void Constructor_WithValidComponents_CreatesVector()
    {
        var vector = new Vector(1, 2, 3);
        Assert.Equal(3, vector.Dimensions);
        Assert.Equal(1, vector[0]);
        Assert.Equal(2, vector[1]);
        Assert.Equal(3, vector[2]);
    }

    [Fact]
    public void Constructor_WithEmptyArray_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Vector());
    }

    [Fact]
    public void Addition_WithSameDimensions_ReturnsSum()
    {
        var a = new Vector(1, 2);
        var b = new Vector(3, 4);
        var result = a + b;
        Assert.Equal(4, result[0]);
        Assert.Equal(6, result[1]);
    }

    [Fact]
    public void Addition_WithDifferentDimensions_ThrowsArgumentException()
    {
        var a = new Vector(1, 2);
        var b = new Vector(1, 2, 3);
        Assert.Throws<ArgumentException>(() => a + b);
    }

    [Fact]
    public void Equality_WithSameComponents_ReturnsTrue()
    {
        var a = new Vector(1, 2, 3);
        var b = new Vector(1, 2, 3);
        Assert.True(a == b);
    }

    [Fact]
    public void Equality_WithDifferentComponents_ReturnsFalse()
    {
        var a = new Vector(1, 2, 3);
        var b = new Vector(1, 2, 4);
        Assert.True(a != b);
    }

    [Fact]
    public void GetHashCode_WithSameComponents_ReturnsSameHash()
    {
        var a = new Vector(1, 2, 3);
        var b = new Vector(1, 2, 3);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }
}
