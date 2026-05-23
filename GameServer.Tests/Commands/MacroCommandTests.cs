#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class MacroCommandTests
{
    [Fact]
    public void Execute_ExecutesAllCommandsInOrder()
    {
        var mockCommand1 = new MockCommand();
        var mockCommand2 = new MockCommand();
        var mockCommand3 = new MockCommand();
        
        var macroCommand = new MacroCommand(new ICommand[] { mockCommand1, mockCommand2, mockCommand3 });
        
        macroCommand.Execute();
        
        Assert.True(mockCommand1.Executed);
        Assert.True(mockCommand2.Executed);
        Assert.True(mockCommand3.Executed);
    }

    [Fact]
    public void Constructor_WhenCommandsIsNull_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new MacroCommand(null));
    }

    [Fact]
    public void Execute_WhenOneCommandThrowsException_StillExecutesPreviousCommands()
    {
        var mockCommand1 = new MockCommand();
        var failingCommand = new FailingCommand();
        var mockCommand2 = new MockCommand();
        
        var macroCommand = new MacroCommand(new ICommand[] { mockCommand1, failingCommand, mockCommand2 });
        
        Assert.Throws<InvalidOperationException>(() => macroCommand.Execute());
        
        Assert.True(mockCommand1.Executed);
        Assert.False(mockCommand2.Executed);
    }

    private class MockCommand : ICommand
    {
        public bool Executed { get; private set; }
        
        public void Execute()
        {
            Executed = true;
        }
    }

    private class FailingCommand : ICommand
    {
        public void Execute()
        {
            throw new InvalidOperationException("Command failed");
        }
    }
}
