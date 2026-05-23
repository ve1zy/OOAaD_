#nullable disable
using GameServer.Commands;
using GameServer.IoC;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterActionsStartCommandTests
{
    [Fact]
    public void Execute_RegistersActionsStartInIoC()
    {
        IoC.Ioc.Clear();
        var mockCommand = new MockCommand();
        IoC.Ioc.Register("TestCommand", (args) => mockCommand);
        
        var registerCommand = new RegisterActionsStartCommand();
        registerCommand.Execute();
        
        var startCommand = (StartCommand)IoC.Ioc.Resolve("Actions.Start", "TestCommand");
        Assert.NotNull(startCommand);
        
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
