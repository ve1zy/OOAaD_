using GameServer.Commands;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyCommandsStopTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersCommandsStopDependency()
    {
        Ioc.Clear();
        var executed = false;
        var stopAction = new Action(() => { executed = true; });

        var registerCommand = new RegisterIoCDependencyCommandsStop();
        registerCommand.Execute();

        var stopCommand = Ioc.Resolve<StopCommand>("Commands.Stop", stopAction);
        Assert.NotNull(stopCommand);
        stopCommand.Execute();

        Assert.True(executed);
    }
}
