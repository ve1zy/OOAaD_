#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using GameServer.Strategies;
using Xunit;

namespace GameServer.Tests.Strategies;

public class CreateMacroCommandStrategyTests
{
    [Fact]
    public void CreateMacroCommand_CreatesMacroCommandFromKeys()
    {
        global::GameServer.IoC.Ioc.Clear();
        var mockCommand1 = new MockCommand();
        var mockCommand2 = new MockCommand();
        global::GameServer.IoC.Ioc.Register("TestCommand1", (args) => mockCommand1);
        global::GameServer.IoC.Ioc.Register("TestCommand2", (args) => mockCommand2);
        
        var strategy = new CreateMacroCommandStrategy();
        var macroCommand = strategy.CreateMacroCommand(new object[] { "TestCommand1", "TestCommand2" });
        
        Assert.NotNull(macroCommand);
        Assert.IsType<MacroCommand>(macroCommand);
        
        macroCommand.Execute();
        Assert.True(mockCommand1.Executed);
        Assert.True(mockCommand2.Executed);
    }

    [Fact]
    public void CreateMacroCommand_WhenKeysIsNull_ThrowsException()
    {
        var strategy = new CreateMacroCommandStrategy();
        Assert.Throws<ArgumentException>(() => strategy.CreateMacroCommand(null));
    }

    [Fact]
    public void CreateMacroCommand_WhenKeysIsEmpty_ThrowsException()
    {
        var strategy = new CreateMacroCommandStrategy();
        Assert.Throws<ArgumentException>(() => strategy.CreateMacroCommand(new object[0]));
    }

    [Fact]
    public void CreateMacroCommand_WhenKeyIsNull_ThrowsException()
    {
        global::GameServer.IoC.Ioc.Clear();
        var strategy = new CreateMacroCommandStrategy();
        Assert.Throws<ArgumentException>(() => strategy.CreateMacroCommand(new object[] { null }));
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
