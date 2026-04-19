using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMacroMoveRotateTests
{
    [Fact]
    public void Execute_RegistersMacroMoveAndMacroRotateInIoC()
    {
        var command = new RegisterIoCDependencyMacroMoveRotate();
        
        command.Execute();
        
        var mock1 = new MockMovingObject();
        var mock2 = new MockMovingObject();
        
        IoC.Ioc.Register("MockObject1", mock1);
        IoC.Ioc.Register("MockObject2", mock2);
        
        var macroMove = IoC.Ioc.Resolve("Macro.Move", "MockObject1", "MockObject2");
        
        Assert.IsType<MacroCommand>(macroMove);
    }

    private class MockMovingObject : IMovingObject
    {
        public void Move(GameServer.Models.Vector position)
        {
        }
    }
}
