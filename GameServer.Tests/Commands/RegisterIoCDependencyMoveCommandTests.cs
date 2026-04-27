using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Commands;

[Collection("IocTests")]
public class RegisterIoCDependencyMoveCommandTests
{
    [Fact]
    public void Execute_RegistersMoveCommandDependency()
    {
        Ioc.Clear();
        var mockObject = new MockMovingObject();
        var position = new Vector(1, 2, 3);
        var velocity = new Vector(4, 5, 6);

        Ioc.Register("Adapters.IMovingObject", (args) => mockObject);
        Ioc.Register("Position", (args) => position);
        Ioc.Register("Velocity", (args) => velocity);

        var command = new RegisterIoCDependencyMoveCommand();
        command.Execute();

        var moveCommand = Ioc.Resolve("Commands.Move", "TestObject");

        Assert.IsType<MoveCommand>(moveCommand);
    }

    private class MockMovingObject : IMovingObject
    {
        public Vector? LastPosition { get; private set; }
        public Vector? LastVelocity { get; private set; }

        public void Move(Vector position, Vector velocity)
        {
            LastPosition = position;
            LastVelocity = velocity;
        }
    }
}
