using GameServer.Commands;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyActionsStartTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersActionsStartDependency()
    {
        Ioc.Clear();
        var executed = false;
        var startAction = new Action(() => { executed = true; });

        var registerCommand = new RegisterIoCDependencyActionsStart();
        registerCommand.Execute();

        var startCommand = Ioc.Resolve<StartCommand>("Actions.Start", startAction);
        Assert.NotNull(startCommand);
        startCommand.Execute();

        Assert.True(executed);
    }
}
