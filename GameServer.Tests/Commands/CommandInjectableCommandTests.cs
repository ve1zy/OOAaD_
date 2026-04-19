using GameServer.Commands;
using GameServer.Interfaces;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class CommandInjectableCommandTests
{
    [Fact]
    public void Execute_AfterCommandIsInjected_CallsInjectedCommand()
    {
        // Arrange
        var command = new CommandInjectableCommand();
        var mockInjectedCommand = new Mock<ICommand>();
        
        command.Inject(mockInjectedCommand.Object);

        // Act
        command.Execute();

        // Assert
        mockInjectedCommand.Verify(x => x.Execute(), Times.Once());
    }

    [Fact]
    public void Execute_WhenNoCommandInjected_ThrowsException()
    {
        // Arrange
        var command = new CommandInjectableCommand();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void Inject_WhenCommandIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var command = new CommandInjectableCommand();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => command.Inject(null!));
    }

    [Fact]
    public void Inject_WhenCalled_SetsInjectedCommand()
    {
        // Arrange
        var command = new CommandInjectableCommand();
        var mockInjectedCommand = new Mock<ICommand>();

        // Act
        command.Inject(mockInjectedCommand.Object);

        // Assert - Execute should work after injection
        command.Execute();
        mockInjectedCommand.Verify(x => x.Execute(), Times.Once());
    }
}
