using GameServer.Commands;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class MacroCommandTests
{
    [Fact]
    public void Constructor_WithNullCommands_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new MacroCommand(null));
    }

    [Fact]
    public void Execute_WithMultipleCommands_ExecutesAllSequentially()
    {
        var mockCommand1 = new Mock<ICommand>();
        var mockCommand2 = new Mock<ICommand>();
        var mockCommand3 = new Mock<ICommand>();

        var macroCommand = new MacroCommand(new[] { mockCommand1.Object, mockCommand2.Object, mockCommand3.Object });
        macroCommand.Execute();

        mockCommand1.Verify(c => c.Execute(), Times.Once);
        mockCommand2.Verify(c => c.Execute(), Times.Once);
        mockCommand3.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_WhenCommandThrowsException_StopsExecution()
    {
        var mockCommand1 = new Mock<ICommand>();
        var mockCommand2 = new Mock<ICommand>();
        mockCommand2.Setup(c => c.Execute()).Throws<Exception>();
        var mockCommand3 = new Mock<ICommand>();

        var macroCommand = new MacroCommand(new[] { mockCommand1.Object, mockCommand2.Object, mockCommand3.Object });
        Assert.Throws<Exception>(() => macroCommand.Execute());

        mockCommand1.Verify(c => c.Execute(), Times.Once);
        mockCommand2.Verify(c => c.Execute(), Times.Once);
        mockCommand3.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void Execute_WithEmptyCommandArray_DoesNothing()
    {
        var macroCommand = new MacroCommand(Array.Empty<ICommand>());
        macroCommand.Execute();
    }
}
