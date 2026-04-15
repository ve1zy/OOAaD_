using GameServer.Commands;
using GameServer.Interfaces;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class SendCommandTests
{
    [Fact]
    public void Constructor_WithNullReceiver_ThrowsArgumentNullException()
    {
        var mockCommand = new Mock<ICommand>();
        Assert.Throws<ArgumentNullException>(() => new SendCommand(null, mockCommand.Object));
    }

    [Fact]
    public void Constructor_WithNullCommand_ThrowsArgumentNullException()
    {
        var mockReceiver = new Mock<ICommandReceiver>();
        Assert.Throws<ArgumentNullException>(() => new SendCommand(mockReceiver.Object, null));
    }

    [Fact]
    public void Execute_WithValidArguments_SendsCommandToReceiver()
    {
        var mockReceiver = new Mock<ICommandReceiver>();
        var mockCommand = new Mock<ICommand>();

        var sendCommand = new SendCommand(mockReceiver.Object, mockCommand.Object);
        sendCommand.Execute();

        mockReceiver.Verify(r => r.Receive(mockCommand.Object), Times.Once);
    }
}
