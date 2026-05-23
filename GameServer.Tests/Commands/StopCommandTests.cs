#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class StopCommandTests
{
    [Fact]
    public void Execute_ExecutesInnerCommand()
    {
        var mockCommand = new MockCommand();
        var stopCommand = new StopCommand(mockCommand);
        
        stopCommand.Execute();
        
        Assert.True(mockCommand.Executed);
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
