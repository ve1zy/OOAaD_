using GameServer.Commands;
using GameServer.Interfaces;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class CommandInjectableCommandTests
{
    [Fact]
    public void InjectCommand_WithNullCommand_ThrowsArgumentNullException()
    {
        var command = new CommandInjectableCommand();
        Assert.Throws<ArgumentNullException>(() => command.InjectCommand(null));
    }

    [Fact]
    public void Execute_WhenNoCommandInjected_ThrowsInvalidOperationException()
    {
        var command = new CommandInjectableCommand();
        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void Execute_WhenCommandInjected_ExecutesInjectedCommand()
    {
        var command = new CommandInjectableCommand();
        var mockInjectedCommand = new Mock<ICommand>();
        command.InjectCommand(mockInjectedCommand.Object);

        command.Execute();

        mockInjectedCommand.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_AfterMultipleInjections_ExecutesLastInjectedCommand()
    {
        var command = new CommandInjectableCommand();
        var mockCommand1 = new Mock<ICommand>();
        var mockCommand2 = new Mock<ICommand>();

        command.InjectCommand(mockCommand1.Object);
        command.InjectCommand(mockCommand2.Object);

        command.Execute();

        mockCommand1.Verify(c => c.Execute(), Times.Never);
        mockCommand2.Verify(c => c.Execute(), Times.Once);
    }
}
