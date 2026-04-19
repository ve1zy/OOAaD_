using GameServer.Commands;
using GameServer.IoC;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMacroCommandTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersCommandsMacroDependency()
    {
        Ioc.Clear();
        var mockCommand1 = new Mock<ICommand>();
        var mockCommand2 = new Mock<ICommand>();
        var commands = new[] { mockCommand1.Object, mockCommand2.Object };

        var registerCommand = new RegisterIoCDependencyMacroCommand();
        registerCommand.Execute();

        var macroCommand = Ioc.Resolve<MacroCommand>("Commands.Macro", commands);
        Assert.NotNull(macroCommand);
        macroCommand.Execute();

        mockCommand1.Verify(c => c.Execute(), Times.Once);
        mockCommand2.Verify(c => c.Execute(), Times.Once);
    }
}
