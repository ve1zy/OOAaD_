using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class MoveCommandTests
{
    [Fact]
    public void Constructor_WithNullMovingObject_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new MoveCommand(null));
    }

    [Fact]
    public void Execute_WithValidMovingObject_UpdatesPosition()
    {
        var mockObject = new Mock<IMovingObject>();
        var position = new Vector(1, 2);
        var velocity = new Vector(3, 4);
        mockObject.Setup(o => o.Position).Returns(position);
        mockObject.Setup(o => o.Velocity).Returns(velocity);

        var command = new MoveCommand(mockObject.Object);
        command.Execute();

        mockObject.Verify(o => o.SetPosition(new Vector(4, 6)), Times.Once);
    }

    [Fact]
    public void Execute_WhenSetPositionFails_ThrowsInvalidOperationException()
    {
        var mockObject = new Mock<IMovingObject>();
        var position = new Vector(1, 2);
        var velocity = new Vector(3, 4);
        mockObject.Setup(o => o.Position).Returns(position);
        mockObject.Setup(o => o.Velocity).Returns(velocity);
        mockObject.Setup(o => o.SetPosition(It.IsAny<Vector>())).Throws<Exception>();

        var command = new MoveCommand(mockObject.Object);
        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }
}
