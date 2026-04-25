using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMoveCommandTests
{
    [Fact]
    public void Execute_RegistersMoveCommandDependency()
    {
        Ioc.Clear();
        var mockObject = new MockMovingObject();
        var position = new Vector(1, 2, 3);

        Ioc.Register("Adapters.IMovingObject", (args) => mockObject);
        Ioc.Register("TestPosition", (args) => position);

        var command = new RegisterIoCDependencyMoveCommand();
        command.Execute();

        var moveCommand = Ioc.Resolve("Commands.Move", "TestObject", "TestPosition");

        Assert.IsType<MoveCommand>(moveCommand);
    }

    private class MockMovingObject : IMovingObject
    {
        public Vector? LastPosition { get; private set; }

        public void Move(Vector position)
        {
            LastPosition = position;
        }
    }
}
