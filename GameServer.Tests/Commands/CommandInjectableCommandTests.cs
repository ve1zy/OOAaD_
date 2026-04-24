#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class CommandInjectableCommandTests
{
    [Fact]
    public void Execute_InjectsCommandIntoInjectable()
    {
        var mockInjectable = new MockCommandInjectable();
        var mockCommand = new MockCommand();
        var command = new CommandInjectableCommand(mockInjectable, mockCommand);
        
        command.Execute();
        
        Assert.Same(mockCommand, mockInjectable.InjectedCommand);
    }

    private class MockCommandInjectable : ICommandInjectable
    {
        public ICommand InjectedCommand { get; private set; }
        
        public void Inject(ICommand command)
        {
            InjectedCommand = command;
        }
    }

    private class MockCommand : ICommand
    {
        public void Execute()
        {
        }
    }
}
