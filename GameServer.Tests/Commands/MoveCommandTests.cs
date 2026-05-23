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
        var command = new MoveCommand(mockObject, position);
        
        command.Execute();
        
        Assert.Equal(position, mockObject.LastPosition);
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
