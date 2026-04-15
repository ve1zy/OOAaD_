using GameServer.Commands;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyActionsStopTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersActionsStopDependency()
    {
        Ioc.Clear();
        var executed = false;
        var stopAction = new Action(() => { executed = true; });

        var registerCommand = new RegisterIoCDependencyActionsStop();
        registerCommand.Execute();

        var stopCommand = Ioc.Resolve<StopCommand>("Actions.Stop", stopAction);
        Assert.NotNull(stopCommand);
        stopCommand.Execute();

        Assert.True(executed);
    }
}
