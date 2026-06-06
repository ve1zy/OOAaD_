#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class SendCommandTests
{
    [Fact]
    public void Execute_SendsCommandToReceiver()
    {
        var innerCommand = new MockCommand();
        var receiver = new MockCommandReceiver();
        var sendCommand = new SendCommand(innerCommand, receiver);

        sendCommand.Execute();

        Assert.Same(innerCommand, receiver.ReceivedCommand);
    }

    [Fact]
    public void Execute_WhenReceiverCannotAccept_ThrowsException()
    {
        var innerCommand = new MockCommand();
        var receiver = new FailingCommandReceiver();
        var sendCommand = new SendCommand(innerCommand, receiver);

        Assert.Throws<InvalidOperationException>(() => sendCommand.Execute());
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

    private class FailingCommandReceiver : ICommandReceiver
    {
        public void Receive(ICommand command)
        {
            throw new InvalidOperationException("Cannot accept command");
        }
    }
}
