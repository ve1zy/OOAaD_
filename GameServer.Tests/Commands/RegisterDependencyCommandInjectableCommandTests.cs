using GameServer.Commands;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterDependencyCommandInjectableCommandTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersCommandsCommandInjectableDependency()
    {
        Ioc.Clear();
        var registerCommand = new RegisterDependencyCommandInjectableCommand();
        registerCommand.Execute();

        var command = Ioc.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");
        Assert.NotNull(command);
    }
}
