using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Commands;

public class MoveCommandTests
{
    [Fact]
    public void Execute_MovesObjectToExpectedPosition()
    {
        var obj = new TestMovingObject
        {
            Position = new Vector(12, 5),
            Velocity = new Vector(-4, 1)
        };
        var command = new MoveCommand(obj);

        command.Execute();

        Assert.Equal(new Vector(8, 6), obj.Position);
    }

    [Fact]
    public void Execute_Throws_WhenPositionCannotBeRead()
    {
        var obj = new PositionReadThrowsMovingObject
        {
            Velocity = new Vector(1, 1)
        };
        var command = new MoveCommand(obj);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void Execute_Throws_WhenVelocityCannotBeRead()
    {
        var obj = new VelocityReadThrowsMovingObject
        {
            Position = new Vector(1, 1)
        };
        var command = new MoveCommand(obj);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void Execute_Throws_WhenPositionCannotBeSet()
    {
        var obj = new PositionSetThrowsMovingObject(new Vector(1, 1))
        {
            Velocity = new Vector(1, 1)
        };
        var command = new MoveCommand(obj);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    private class TestMovingObject : IMovingObject
    {
        public Vector Position { get; set; } = new(0, 0);

        public Vector Velocity { get; set; } = new(0, 0);

        Vector IMovingObject.Velocity => Velocity;
    }

    private class PositionReadThrowsMovingObject : IMovingObject
    {
        public Vector Position
        {
            get => throw new InvalidOperationException();
            set => throw new NotSupportedException();
        }

        public Vector Velocity { get; set; } = new(0, 0);

        Vector IMovingObject.Velocity => Velocity;
    }

    private class VelocityReadThrowsMovingObject : IMovingObject
    {
        public Vector Position { get; set; } = new(0, 0);

        public Vector Velocity => throw new InvalidOperationException();
    }

    private class PositionSetThrowsMovingObject : IMovingObject
    {
        private Vector _position;

        public PositionSetThrowsMovingObject(Vector initialPosition)
        {
            _position = initialPosition;
        }

        public Vector Position
        {
            get => _position;
            set => throw new InvalidOperationException();
        }

        public Vector Velocity { get; set; } = new(0, 0);

        Vector IMovingObject.Velocity => Velocity;
    }
}
