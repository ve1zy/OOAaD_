using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencySendCommandTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersCommandsSendDependency()
    {
        Ioc.Clear();
        var mockReceiver = new Mock<ICommandReceiver>();
        var mockCommand = new Mock<ICommand>();

        var registerCommand = new RegisterIoCDependencySendCommand();
        registerCommand.Execute();

        var sendCommand = Ioc.Resolve<SendCommand>("Commands.Send", mockReceiver.Object, "Commands.Move");
        Assert.NotNull(sendCommand);
    }
}
