using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using GameServer.Models;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyRotateCommandTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersCommandsRotateDependency()
    {
        Ioc.Clear();
        Angle.CommonDenominator = 360;
        var mockRotatingObject = new Mock<IRotatingObject>();
        mockRotatingObject.Setup(o => o.Angle).Returns(new Angle(0));
        mockRotatingObject.Setup(o => o.AngularVelocity).Returns(new Angle(90));
        mockRotatingObject.Setup(o => o.SetAngle(It.IsAny<Angle>()));

        var registerCommand = new RegisterIoCDependencyRotateCommand();
        registerCommand.Execute();

        var rotateCommand = Ioc.Resolve<RotateCommand>("Commands.Rotate", mockRotatingObject.Object);
        Assert.NotNull(rotateCommand);
        rotateCommand.Execute();

        mockRotatingObject.Verify(o => o.SetAngle(new Angle(90)), Times.Once);
    }
}
