using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Commands;

public class RotateCommandTests
{
    [Fact]
    public void Execute_CallsRotateOnObject()
    {
        var mockObject = new MockRotatingObject();
        var angle = new Angle(90);
        var command = new RotateCommand(mockObject, angle);
        
        command.Execute();
        
        Assert.Equal(angle, mockObject.LastAngle);
    }

    private class MockRotatingObject : IRotatingObject
    {
        public Angle? LastAngle { get; private set; }
        
        public void Rotate(Angle angle)
        {
            LastAngle = angle;
        }
    }
}
