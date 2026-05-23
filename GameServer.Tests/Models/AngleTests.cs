#nullable disable

using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Models;

public class AngleTests
{
    [Fact]
    public void Constructor_WithValidArguments_CreatesAngle()
    {
        var angle = new Angle(1, 4);
        Assert.Equal(1, angle.Numerator);
        Assert.Equal(4, angle.Denominator);
    }

    [Fact]
    public void Constructor_WithZeroDenominator_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Angle(1, 0));
    }

    [Fact]
    public void Constructor_NormalizesFraction()
    {
        var angle = new Angle(2, 8);
        Assert.Equal(1, angle.Numerator);
        Assert.Equal(4, angle.Denominator);
    }

    [Fact]
    public void Constructor_MakesDenominatorPositive()
    {
        var angle = new Angle(1, -4);
        Assert.Equal(-1, angle.Numerator);
        Assert.Equal(4, angle.Denominator);
    }

    [Fact]
    public void Degrees_ReturnsCorrectValue()
    {
        var angle = new Angle(1, 4);
        Assert.Equal(90, angle.Degrees);
    }

    [Fact]
    public void AdditionOperator_AddsAngles()
    {
        var a = new Angle(1, 4);
        var b = new Angle(1, 4);
        var result = a + b;
        Assert.Equal(1, result.Numerator);
        Assert.Equal(2, result.Denominator);
    }

    [Fact]
    public void SubtractionOperator_SubtractsAngles()
    {
        var a = new Angle(1, 2);
        var b = new Angle(1, 4);
        var result = a - b;
        Assert.Equal(1, result.Numerator);
        Assert.Equal(4, result.Denominator);
    }

    [Fact]
    public void EqualityOperator_WithEqualAngles_ReturnsTrue()
    {
        var a = new Angle(1, 4);
        var b = new Angle(2, 8);
        Assert.True(a == b);
    }

    [Fact]
    public void EqualityOperator_WithDifferentAngles_ReturnsFalse()
    {
        var a = new Angle(1, 4);
        var b = new Angle(1, 2);
        Assert.False(a == b);
    }

    [Fact]
    public void InequalityOperator_WithEqualAngles_ReturnsFalse()
    {
        var a = new Angle(1, 4);
        var b = new Angle(2, 8);
        Assert.False(a != b);
    }

    [Fact]
    public void InequalityOperator_WithDifferentAngles_ReturnsTrue()
    {
        var a = new Angle(1, 4);
        var b = new Angle(1, 2);
        Assert.True(a != b);
    }

    [Fact]
    public void Equals_WithEqualAngles_ReturnsTrue()
    {
        var a = new Angle(1, 4);
        var b = new Angle(2, 8);
        Assert.True(a.Equals(b));
    }

    [Fact]
    public void Equals_WithDifferentAngles_ReturnsFalse()
    {
        var a = new Angle(1, 4);
        var b = new Angle(1, 2);
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void GetHashCode_WithEqualAngles_ReturnsSameHash()
    {
        var a = new Angle(1, 4);
        var b = new Angle(2, 8);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Sin_ReturnsCorrectValue()
    {
        var angle = new Angle(1, 2); // 180 degrees
        Assert.Equal(0, angle.Sin(), 5);
    }

    [Fact]
    public void Cos_ReturnsCorrectValue()
    {
        var angle = new Angle(0, 1); // 0 degrees
        Assert.Equal(1, angle.Cos(), 5);
    }

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        var angle = new Angle(1, 4);
        Assert.Equal("1/4π", angle.ToString());
    }
}
