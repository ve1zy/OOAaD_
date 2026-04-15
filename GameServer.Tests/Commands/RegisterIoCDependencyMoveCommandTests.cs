using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using GameServer.Models;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMoveCommandTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersCommandsMoveDependency()
    {
        Ioc.Clear();
        var mockMovingObject = new Mock<IMovingObject>();
        mockMovingObject.Setup(o => o.Position).Returns(new Vector(0, 0));
        mockMovingObject.Setup(o => o.Velocity).Returns(new Vector(1, 1));
        mockMovingObject.Setup(o => o.SetPosition(It.IsAny<Vector>()));

        var registerCommand = new RegisterIoCDependencyMoveCommand();
        registerCommand.Execute();

        var moveCommand = Ioc.Resolve<MoveCommand>("Commands.Move", mockMovingObject.Object);
        Assert.NotNull(moveCommand);
        moveCommand.Execute();

        mockMovingObject.Verify(o => o.SetPosition(new Vector(1, 1)), Times.Once);
    }
}
