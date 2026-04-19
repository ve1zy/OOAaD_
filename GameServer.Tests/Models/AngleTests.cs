using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Models;

public class AngleTests
{
    [Fact]
    public void Constructor_WithNumerator_CreatesAngle()
    {
        var angle = new Angle(90);
        Assert.Equal(90, angle.Numerator);
    }

    [Fact]
    public void Addition_WithTwoAngles_ReturnsSum()
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
    public void Equality_WithSameComponents_ReturnsTrue()
    {
        var a = new Angle(90);
        var b = new Angle(90);
        Assert.True(a == b);
    }

    [Fact]
    public void Equality_WithDifferentComponents_ReturnsFalse()
    {
        var a = new Angle(90);
        var b = new Angle(180);
        Assert.True(a != b);
    }

    [Fact]
    public void GetHashCode_WithSameComponents_ReturnsSameHash()
    {
        var a = new Angle(90);
        var b = new Angle(90);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void ToRadians_With90Degrees_ReturnsHalfPi()
    {
        Angle.CommonDenominator = 360;
        var angle = new Angle(90);
        var radians = angle.ToRadians();
        Assert.Equal(Math.PI / 2, radians, 4);
    }

    [Fact]
    public void AngleMath_Sin_With90Degrees_Returns1()
    {
        Angle.CommonDenominator = 360;
        var angle = new Angle(90);
        var sin = AngleMath.Sin(angle);
        Assert.Equal(1, sin, 4);
    }

    [Fact]
    public void AngleMath_Cos_With0Degrees_Returns1()
    {
        Angle.CommonDenominator = 360;
        var angle = new Angle(0);
        var cos = AngleMath.Cos(angle);
        Assert.Equal(1, cos, 4);
    }
}
