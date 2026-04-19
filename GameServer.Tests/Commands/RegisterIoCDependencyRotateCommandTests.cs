using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyRotateCommandTests
{
    [Fact]
    public void Execute_RegistersRotateCommandInIoC()
    {
        var command = new RegisterIoCDependencyRotateCommand();
        
        command.Execute();
        
        var mockObject = new MockRotatingObject();
        var angle = new Angle(90);
        
        IoC.Ioc.Register("MockRotatingObject", mockObject);
        IoC.Ioc.Register("MockAngle", angle);
        
        var rotateCommand = IoC.Ioc.Resolve("Commands.Rotate", "MockRotatingObject", "MockAngle");
        
        Assert.IsType<RotateCommand>(rotateCommand);
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
