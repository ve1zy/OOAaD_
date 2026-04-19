using GameServer.Commands;
using GameServer.Interfaces;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMacroCommandTests
{
    [Fact]
    public void Execute_RegistersMacroCommandInIoC()
    {
        var command = new RegisterIoCDependencyMacroCommand();
        
        command.Execute();
        
        var mock1 = new MockCommand();
        var mock2 = new MockCommand();
        
        IoC.Ioc.Register("MockCommand1", mock1);
        IoC.Ioc.Register("MockCommand2", mock2);
        
        var macroCommand = IoC.Ioc.Resolve("Commands.Macro", "MockCommand1", "MockCommand2");
        
        Assert.IsType<MacroCommand>(macroCommand);
    }

    private class MockCommand : ICommand
    {
        public void Execute()
        {
        }
    }
}
