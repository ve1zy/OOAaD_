#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterMacroMoveRotateCommandTests
{
    [Fact]
    public void Execute_RegistersMacroMoveAndMacroRotateInIoC()
    {
        IoC.Ioc.Clear();
        var mockMovableObject = new MockMovableObject();
        var mockRotatingObject = new MockRotatingObject();
        
        IoC.Ioc.Register("TestMovableObject", (args) => mockMovableObject);
        IoC.Ioc.Register("TestRotatingObject", (args) => mockRotatingObject);
        
        var registerCommand = new RegisterMacroMoveRotateCommand();
        registerCommand.Execute();
        
        var macroMove = (MacroCommand)IoC.Ioc.Resolve("Macro.Move", "TestMovableObject");
        Assert.NotNull(macroMove);
        
        var macroRotate = (MacroCommand)IoC.Ioc.Resolve("Macro.Rotate", "TestRotatingObject");
        Assert.NotNull(macroRotate);
    }

    private class MockMovableObject : IMovingObject
    {
        public Vector Position { get; set; } = new Vector(0, 0);
        public Vector Velocity { get; set; } = new Vector(1, 0);
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
