#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterActionsStopCommandTests
{
    [Fact]
    public void Execute_RegistersActionsStopInIoC()
    {
        IoC.Ioc.Clear();
        var mockCommand = new MockCommand();
        IoC.Ioc.Register("TestCommand", (args) => mockCommand);
        
        var registerCommand = new RegisterActionsStopCommand();
        registerCommand.Execute();
        
        var stopCommand = (StopCommand)IoC.Ioc.Resolve("Actions.Stop", "TestCommand");
        Assert.NotNull(stopCommand);
        
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
