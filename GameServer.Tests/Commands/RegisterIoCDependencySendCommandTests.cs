#nullable disable
using GameServer.Commands;
using GameServer.IoC;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencySendCommandTests
{
    [Fact]
    public void Execute_RegistersSendCommandInIoC()
    {
        IoC.Ioc.Clear();
        var mockReceiver = new MockCommandReceiver();
        var mockCommand = new MockCommand();
        IoC.Ioc.Register("TestReceiver", (args) => mockReceiver);
        IoC.Ioc.Register("TestCommand", (args) => mockCommand);
        
        var registerCommand = new RegisterIoCDependencySendCommand();
        registerCommand.Execute();
        
        var sendCommand = (SendCommand)IoC.Ioc.Resolve("Commands.Send", "TestReceiver", "TestCommand");
        Assert.NotNull(sendCommand);
        
        sendCommand.Execute();
        Assert.Same(mockCommand, mockReceiver.ReceivedCommand);
    }

    private class MockCommandReceiver : ICommandReceiver
    {
        public ICommand ReceivedCommand { get; private set; }
        
        public void Receive(ICommand command)
        {
            ReceivedCommand = command;
        }
    }

    private class MockCommand : ICommand
    {
        public void Execute()
        {
        }
    }
}
