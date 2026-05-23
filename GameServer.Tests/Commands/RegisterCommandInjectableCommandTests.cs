#nullable disable
using GameServer.Commands;
using GameServer.IoC;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterCommandInjectableCommandTests
{
    [Fact]
    public void Execute_RegistersCommandInjectableInIoC()
    {
        IoC.Ioc.Clear();
        var mockInjectable = new MockCommandInjectable();
        var mockCommand = new MockCommand();
        IoC.Ioc.Register("TestInjectable", (args) => mockInjectable);
        IoC.Ioc.Register("TestCommand", (args) => mockCommand);
        
        var registerCommand = new RegisterCommandInjectableCommand();
        registerCommand.Execute();
        
        var commandInjectable = (CommandInjectableCommand)IoC.Ioc.Resolve("Commands.CommandInjectable", "TestInjectable", "TestCommand");
        Assert.NotNull(commandInjectable);
        
        commandInjectable.Execute();
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
