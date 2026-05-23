#nullable disable
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
        IoC.Ioc.Clear();
        var mockObject = new MockRotatingObject();
        IoC.Ioc.Register("TestRotatingObject", (args) => mockObject);
        
        var registerCommand = new RegisterIoCDependencyRotateCommand();
        registerCommand.Execute();
        
        var rotateCommand = (RotateCommand)IoC.Ioc.Resolve("Commands.Rotate", "TestRotatingObject");
        Assert.NotNull(rotateCommand);
    }

    private class MockRotatingObject : IRotatingObject
    {
        public Angle Angle { get; set; }
        public Angle AngularVelocity { get; set; }
        
        public void SetAngle(Angle angle)
        {
            Angle = angle;
        }
    }
}
