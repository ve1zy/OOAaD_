using GameServer.Interfaces;
using GameServer.Strategies;
using Xunit;

namespace GameServer.Tests.Strategies;

public class CreateMacroCommandStrategyTests
{
    [Fact]
    public void CreateMacroCommand_WithCommandNames_ResolvesAndCreatesMacro()
    {
        var strategy = new CreateMacroCommandStrategy();
        var mock1 = new MockCommand();
        var mock2 = new MockCommand();
        
        IoC.Ioc.Register("MockCommand1", mock1);
        IoC.Ioc.Register("MockCommand2", mock2);
        
        var macroCommand = strategy.CreateMacroCommand(new[] { "MockCommand1", "MockCommand2" });
        
        Assert.IsType<Commands.MacroCommand>(macroCommand);
    }

    private class MockCommand : ICommand
    {
        public void Execute()
        {
        }
    }
}
