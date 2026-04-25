using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using Xunit;

namespace GameServer.Tests.Commands;

public class MoveCommandTests
{
    [Fact]
    public void Execute_CallsMoveOnObject()
    {
        var mockObject = new MockMovingObject();
        var position = new Vector(1, 2, 3);
        var velocity = new Vector(4, 5, 6);
        var command = new MoveCommand(mockObject, position, velocity);
        
        command.Execute();
        
        Assert.Equal(position, mockObject.LastPosition);
        Assert.Equal(velocity, mockObject.LastVelocity);
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
