using GameServer.Commands;
using Xunit;

namespace GameServer.Tests.Commands;

public class StartCommandTests
{
    [Fact]
    public void Constructor_WithNullAction_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new StartCommand(null));
    }

    [Fact]
    public void Execute_WhenCalled_ExecutesStartAction()
    {
        var executed = false;
        var command = new StartCommand(() => { executed = true; });
        command.Execute();

        Assert.True(executed);
    }
}
