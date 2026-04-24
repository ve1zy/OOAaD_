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
        var mockReceiver = new MockCommandReceiver();
        var mockCommand = new MockCommand();
        var sendCommand = new SendCommand(mockReceiver, mockCommand);
        
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
