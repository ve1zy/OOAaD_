using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterDependencyCommandInjectableCommandTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersCommandsCommandInjectableDependency()
    {
        // Arrange
        Ioc.Clear();
        var command = new RegisterDependencyCommandInjectableCommand();

        // Act
        command.Execute();

        // Assert - All three resolves should work without exceptions
        var asICommand = Ioc.Resolve<ICommand>("Commands.CommandInjectable");
        Assert.NotNull(asICommand);
        Assert.IsType<CommandInjectableCommand>(asICommand);

        var asICommandInjectable = Ioc.Resolve<ICommandInjectable>("Commands.CommandInjectable");
        Assert.NotNull(asICommandInjectable);
        Assert.IsType<CommandInjectableCommand>(asICommandInjectable);

        var asCommandInjectableCommand = Ioc.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
        Assert.NotNull(asCommandInjectableCommand);
    }
}
