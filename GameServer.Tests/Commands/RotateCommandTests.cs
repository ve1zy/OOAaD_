using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class RotateCommandTests
{
    [Fact]
    public void Constructor_WithNullRotatingObject_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new RotateCommand(null));
    }

    [Fact]
    public void Execute_WithValidRotatingObject_UpdatesAngle()
    {
        Angle.CommonDenominator = 360;
        var mockObject = new Mock<IRotatingObject>();
        var angle = new Angle(90);
        var angularVelocity = new Angle(45);
        mockObject.Setup(o => o.Angle).Returns(angle);
        mockObject.Setup(o => o.AngularVelocity).Returns(angularVelocity);

        var command = new RotateCommand(mockObject.Object);
        command.Execute();

        mockObject.Verify(o => o.SetAngle(new Angle(135)), Times.Once);
    }

    [Fact]
    public void Execute_WhenSetAngleFails_ThrowsInvalidOperationException()
    {
        Angle.CommonDenominator = 360;
        var mockObject = new Mock<IRotatingObject>();
        var angle = new Angle(90);
        var angularVelocity = new Angle(45);
        mockObject.Setup(o => o.Angle).Returns(angle);
        mockObject.Setup(o => o.AngularVelocity).Returns(angularVelocity);
        mockObject.Setup(o => o.SetAngle(It.IsAny<Angle>())).Throws<Exception>();

        var command = new RotateCommand(mockObject.Object);
        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }
}
