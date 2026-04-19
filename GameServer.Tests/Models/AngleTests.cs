using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Models;

public class AngleTests
{
    [Fact]
    public void Constructor_WithValidValue_CreatesAngle()
    {
        var angle = new Angle(90);
        Assert.Equal(90, angle.Numerator);
    }

    [Fact]
    public void Addition_WithValidAngles_ReturnsSum()
    {
        var a = new Angle(90);
        var b = new Angle(180);
        var result = a + b;
        Assert.Equal(270, result.Numerator);
    }

    [Fact]
    public void Addition_WithOverflow_Normalizes()
    {
        var a = new Angle(180);
        var b = new Angle(200);
        var result = a + b;
        Assert.Equal(20, result.Numerator);
    }

    [Fact]
    public void Equality_WithSameValues_ReturnsTrue()
    {
        var a = new Angle(90);
        var b = new Angle(90);
        Assert.True(a == b);
    }

    [Fact]
    public void Equality_WithDifferentValues_ReturnsFalse()
    {
        var a = new Angle(90);
        var b = new Angle(180);
        Assert.True(a != b);
    }

    [Fact]
    public void Equality_WithNormalizedValues_ReturnsTrue()
    {
        var a = new Angle(90);
        var b = new Angle(450);
        Assert.True(a == b);
    }

    [Fact]
    public void GetHashCode_WithSameValues_ReturnsSameHash()
    {
        var a = new Angle(90);
        var b = new Angle(90);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void ToString_ReturnsFraction()
    {
        var angle = new Angle(180);
        Assert.Equal("180/360", angle.ToString());
    }
}
