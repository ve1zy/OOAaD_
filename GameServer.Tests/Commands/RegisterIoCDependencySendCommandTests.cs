#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencySendCommandTests
{
    [Fact]
    public void Execute_RegistersSendCommandInIoC()
    {
        Ioc.Clear();
        var mockCommand = new MockCommand();
        var receiver = new MockCommandReceiver();

        var registerCommand = new RegisterIoCDependencySendCommand();
        registerCommand.Execute();

        var sendCommand = (SendCommand)Ioc.Resolve("Commands.Send", mockCommand, receiver);
        Assert.NotNull(sendCommand);

        sendCommand.Execute();
        Assert.Same(mockCommand, receiver.ReceivedCommand);
    }

    private class MockCommand : ICommand
    {
        public void Execute()
        {
        }
    }

    private class MockCommandReceiver : ICommandReceiver
    {
        public ICommand ReceivedCommand { get; private set; }

        public void Receive(ICommand command)
        {
            ReceivedCommand = command;
        }
    }
}
