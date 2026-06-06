#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMacroCommandTests
{
    [Fact]
    public void Execute_RegistersMacroCommandInIoC()
    {
        Ioc.Clear();
        var mock1 = new MockCommand();
        var mock2 = new MockCommand();
        var commands = new ICommand[] { mock1, mock2 };

        var registerCommand = new RegisterIoCDependencyMacroCommand();
        registerCommand.Execute();

        var macroCommand = (MacroCommand)Ioc.Resolve("Commands.Macro", new object[] { commands });
        Assert.NotNull(macroCommand);

        macroCommand.Execute();
        Assert.True(mock1.Executed);
        Assert.True(mock2.Executed);
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
