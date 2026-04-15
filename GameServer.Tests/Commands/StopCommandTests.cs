using GameServer.Commands;
using Xunit;

namespace GameServer.Tests.Commands;

public class StopCommandTests
{
    [Fact]
    public void Constructor_WithNullAction_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new StopCommand(null));
    }

    [Fact]
    public void Execute_WhenCalled_ExecutesStopAction()
    {
        var executed = false;
        var command = new StopCommand(() => { executed = true; });
        command.Execute();

        Assert.True(executed);
    }
}
