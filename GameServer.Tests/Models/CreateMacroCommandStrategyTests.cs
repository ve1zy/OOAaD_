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
    public void Resolve_AllDependenciesPresent_ReturnsMacroCommand()
    {
        Ioc.Clear();

        var mock1 = new MockCommand();
        var mock2 = new MockCommand();

        Ioc.Register("Specs.Test", new string[] { "Command.1", "Command.2" });
        Ioc.Register("Command.1", mock1);
        Ioc.Register("Command.2", mock2);

        var strategy = new CreateMacroCommandStrategy("Test");
        var macro = strategy.Resolve();

        Assert.NotNull(macro);

        macro.Execute();
        Assert.True(mock1.Executed);
        Assert.True(mock2.Executed);
    }

    [Fact]
    public void Resolve_MissingDependency_ThrowsException()
    {
        Ioc.Clear();

        var strategy = new CreateMacroCommandStrategy("Test");
        Assert.Throws<InvalidOperationException>(() => strategy.Resolve());

        Ioc.Register("Specs.Test", new string[] { "Command.Missing" });
        Assert.Throws<InvalidOperationException>(() => strategy.Resolve());
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
