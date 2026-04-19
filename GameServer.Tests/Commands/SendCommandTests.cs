using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class SendCommandTests
{
    [Fact]
    public void Execute_CallsReceiveOnReceiver()
    {
        var mockReceiver = new MockCommandReceiver();
        var mockCommand = new MockCommand();
        var sendCommand = new SendCommand(mockReceiver, mockCommand);
        
        sendCommand.Execute();
        
        Assert.Same(mockCommand, mockReceiver.LastReceivedCommand);
    }

    private class MockCommandReceiver : ICommandReceiver
    {
        public ICommand? LastReceivedCommand { get; private set; }
        
        public void Receive(ICommand command)
        {
            LastReceivedCommand = command;
        }
    }

    private class MockCommand : ICommand
    {
        public void Execute()
        {
        }
    }
}
