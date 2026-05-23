#nullable disable
using GameServer.Commands;
using GameServer.IoC;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterCommandsStopCommandTests
{
    [Fact]
    public void Execute_RegistersCommandsStopInIoC()
    {
        IoC.Ioc.Clear();
        var mockCommand = new MockCommand();
        IoC.Ioc.Register("TestCommand", (args) => mockCommand);
        
        var registerCommand = new RegisterCommandsStopCommand();
        registerCommand.Execute();
        
        var stopCommand = (StopCommand)IoC.Ioc.Resolve("Commands.Stop", "TestCommand");
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
