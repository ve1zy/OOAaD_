#nullable disable

using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMoveCommandTests
{
    [Fact]
    public void Execute_RegistersMoveCommandStrategy()
    {
        var command = new RegisterIoCDependencyMoveCommand();
        
        command.Execute();
        
        var movableObject = new MockMovingObject();
        var position = new Vector(1, 2, 3);
        
        Ioc.Register("TestMovableObject", movableObject);
        Ioc.Register("TestPosition", position);
        
        var moveCommand = Ioc.Resolve("Commands.Move", new object[] { "TestMovableObject", "TestPosition" });
        
        Assert.NotNull(moveCommand);
        Assert.IsType<MoveCommand>(moveCommand);
        
        var typedCommand = (MoveCommand)moveCommand;
        typedCommand.Execute();
        
        Assert.Equal(position, movableObject.LastPosition);
    }

    [Fact]
    public void Execute_WithNullArgs_ThrowsArgumentException()
    {
        var command = new RegisterIoCDependencyMoveCommand();
        command.Execute();
        
        Assert.Throws<ArgumentException>(() => Ioc.Resolve("Commands.Move", Array.Empty<object>()));
    }

    [Fact]
    public void Execute_WithInsufficientArgs_ThrowsArgumentException()
    {
        var command = new RegisterIoCDependencyMoveCommand();
        command.Execute();
        
        Assert.Throws<ArgumentException>(() => Ioc.Resolve("Commands.Move", new object[] { "onlyOneArg" }));
    }

    [Fact]
    public void Execute_WithNonStringKeys_ThrowsArgumentException()
    {
        var command = new RegisterIoCDependencyMoveCommand();
        command.Execute();
        
        Assert.Throws<ArgumentException>(() => Ioc.Resolve("Commands.Move", new object[] { 123, 456 }));
    }

    private class MockMovingObject : IMovingObject
    {
        public Vector LastPosition { get; private set; }
        
        public void Move(Vector position)
        {
            LastPosition = position;
        }
    }
}
