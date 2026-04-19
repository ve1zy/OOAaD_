using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class MacroCommandTests
{
    [Fact]
    public void Execute_RunsAllCommands()
    {
        var mock1 = new MockCommand();
        var mock2 = new MockCommand();
        var mock3 = new MockCommand();
        var macro = new MacroCommand(new ICommand[] { mock1, mock2, mock3 });
        
        macro.Execute();
        
        Assert.True(mock1.Executed);
        Assert.True(mock2.Executed);
        Assert.True(mock3.Executed);
    }

    [Fact]
    public void Execute_WithEmptyCommands_DoesNothing()
    {
        var macro = new MacroCommand(Array.Empty<ICommand>());
        
        macro.Execute();
    }

    private class MockCommand : ICommand
    {
        public bool Executed { get; private set; }
        
        public void Execute()
        {
            Executed = true;
        }
    }
}
