#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMacroCommandTests
{
    [Fact]
    public void Execute_RegistersMacroCommandInIoC()
    {
        global::GameServer.IoC.Ioc.Clear();
        var mockCommand1 = new MockCommand();
        var mockCommand2 = new MockCommand();
        global::GameServer.IoC.Ioc.Register("TestCommand1", (args) => mockCommand1);
        global::GameServer.IoC.Ioc.Register("TestCommand2", (args) => mockCommand2);
        
        var registerCommand = new RegisterIoCDependencyMacroCommand();
        registerCommand.Execute();
        
        var macroCommand = (MacroCommand)global::GameServer.IoC.Ioc.Resolve("Commands.Macro", "TestCommand1", "TestCommand2");
        Assert.NotNull(macroCommand);
        
        macroCommand.Execute();
        Assert.True(mockCommand1.Executed);
        Assert.True(mockCommand2.Executed);
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
