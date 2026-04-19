using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencySendCommandTests
{
    [Fact]
    public void Execute_RegistersSendCommandInIoC()
    {
        var command = new RegisterIoCDependencySendCommand();
        
        command.Execute();
        
        var mockReceiver = new MockCommandReceiver();
        var mockCommand = new MockCommand();
        
        IoC.Ioc.Register("MockReceiver", mockReceiver);
        IoC.Ioc.Register("MockCommand", mockCommand);
        
        var sendCommand = IoC.Ioc.Resolve("Commands.Send", "MockReceiver", "MockCommand");
        
        Assert.IsType<SendCommand>(sendCommand);
    }

    private class MockCommandReceiver : ICommandReceiver
    {
        public void Receive(ICommand command)
        {
        }
    }

    private class MockCommand : ICommand
    {
        public void Execute()
        {
        }
    }
}
