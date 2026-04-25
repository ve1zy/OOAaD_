#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
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
        var position = new Vector(1, 2, 3);
        
        IoC.Ioc.Register("TestMovableObject", (args) => mockMovableObject);
        IoC.Ioc.Register("TestPosition", (args) => position);
        IoC.Ioc.Register("TestRotatingObject", (args) => mockRotatingObject);
        
        var registerCommand = new RegisterMacroMoveRotateCommand();
        registerCommand.Execute();
        
        var macroMove = (MacroCommand)IoC.Ioc.Resolve("Macro.Move", "TestMovableObject", "TestPosition");
        Assert.NotNull(macroMove);
        
        var macroRotate = (MacroCommand)IoC.Ioc.Resolve("Macro.Rotate", "TestRotatingObject");
        Assert.NotNull(macroRotate);
    }

    private class MockMovableObject : IMovingObject
    {
        public Vector? LastPosition { get; private set; }
        
        public void Move(Vector position)
        {
            LastPosition = position;
        }
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
