#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class StartCommandTests
{
    [Fact]
    public void Execute_ExecutesInnerCommand()
    {
        var mockCommand = new MockCommand();
        var startCommand = new StartCommand(mockCommand);
        
        startCommand.Execute();
        
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
