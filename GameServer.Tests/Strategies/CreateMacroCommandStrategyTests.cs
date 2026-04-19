using GameServer.Commands;
using GameServer.IoC;
using GameServer.Strategies;
using Moq;
using Xunit;

namespace GameServer.Tests.Strategies;

public class CreateMacroCommandStrategyTests
{
    [Fact]
    public void Resolve_WithValidSpec_ReturnsMacroCommand()
    {
        Ioc.Clear();
        var mockCommand1 = new Mock<ICommand>();
        var mockCommand2 = new Mock<ICommand>();
        var commandNames = new[] { "Commands.Move", "Commands.Rotate" };

        Ioc.Register("Specs.Macro.Test", _ => commandNames);
        Ioc.Register("Commands.Move", _ => mockCommand1.Object);
        Ioc.Register("Commands.Rotate", _ => mockCommand2.Object);

        var macroCommand = CreateMacroCommandStrategy.Resolve("Macro.Test");
        Assert.NotNull(macroCommand);
        macroCommand.Execute();

        mockCommand1.Verify(c => c.Execute(), Times.Once);
        mockCommand2.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Resolve_WithEmptySpec_ThrowsInvalidOperationException()
    {
        Ioc.Clear();
        Ioc.Register("Specs.Macro.Test", _ => Array.Empty<string>());

        Assert.Throws<InvalidOperationException>(() => CreateMacroCommandStrategy.Resolve("Macro.Test"));
    }

    [Fact]
    public void Resolve_WhenCommandCannotBeResolved_ThrowsInvalidOperationException()
    {
        Ioc.Clear();
        var commandNames = new[] { "Commands.Unknown" };

        Ioc.Register("Specs.Macro.Test", _ => commandNames);

        Assert.Throws<InvalidOperationException>(() => CreateMacroCommandStrategy.Resolve("Macro.Test"));
    }
}
