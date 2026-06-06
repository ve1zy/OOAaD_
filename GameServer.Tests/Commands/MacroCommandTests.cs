#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class MacroCommandTests
{
    [Fact]
    public void Execute_ExecutesAllCommands()
    {
        var mock1 = new MockCommand();
        var mock2 = new MockCommand();
        var mock3 = new MockCommand();
        var commands = new ICommand[] { mock1, mock2, mock3 };
        var macro = new MacroCommand(commands);

        macro.Execute();

        Assert.True(mock1.Executed);
        Assert.True(mock2.Executed);
        Assert.True(mock3.Executed);
    }

    [Fact]
    public void Execute_WhenCommandThrows_StopsExecution()
    {
        var mock1 = new MockCommand();
        var failingCommand = new FailingCommand();
        var mock2 = new MockCommand();
        var commands = new ICommand[] { mock1, failingCommand, mock2 };
        var macro = new MacroCommand(commands);

        Assert.Throws<InvalidOperationException>(() => macro.Execute());

        Assert.True(mock1.Executed);
        Assert.False(mock2.Executed);
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
