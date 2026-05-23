#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Commands;

public class RotateCommandTests
{
    [Fact]
    public void Execute_45DegreesPlus45DegreesEquals90Degrees()
    {
        var mockObject = new MockRotatingObject
        {
            Angle = new Angle(1, 8), // 45 degrees (1/8 * 360 = 45)
            AngularVelocity = new Angle(1, 8) // 45 degrees
        };
        var command = new RotateCommand(mockObject);
        
        command.Execute();
        
        Assert.Equal(90, mockObject.Angle.Degrees, 0.01);
    }

    [Fact]
    public void Execute_WhenAngleCannotBeDetermined_ThrowsException()
    {
        var mockObject = new MockRotatingObject
        {
            Angle = null,
            AngularVelocity = new Angle(1, 8)
        };
        var command = new RotateCommand(mockObject);
        
        var exception = Assert.Throws<ArgumentException>(() => command.Execute());
        Assert.Contains("Cannot determine angle", exception.Message);
    }

    [Fact]
    public void Execute_WhenAngularVelocityCannotBeDetermined_ThrowsException()
    {
        var mockObject = new MockRotatingObject
        {
            Angle = new Angle(1, 8),
            AngularVelocity = null
        };
        var command = new RotateCommand(mockObject);
        
        var exception = Assert.Throws<ArgumentException>(() => command.Execute());
        Assert.Contains("Cannot determine angular velocity", exception.Message);
    }

    [Fact]
    public void Execute_WhenAngleCannotBeChanged_ThrowsException()
    {
        var mockObject = new MockRotatingObject
        {
            Angle = new Angle(1, 8),
            AngularVelocity = new Angle(1, 8),
            CanSetAngle = false
        };
        var command = new RotateCommand(mockObject);
        
        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    private class MockRotatingObject : IRotatingObject
    {
        public Angle Angle { get; set; }
        public Angle AngularVelocity { get; set; }
        public bool CanSetAngle { get; set; } = true;
        
        public void SetAngle(Angle angle)
        {
            if (!CanSetAngle)
            {
                throw new InvalidOperationException("Cannot set angle");
            }
            Angle = angle;
        }
    }
}
