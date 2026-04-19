using GameServer.Commands;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMacroMoveRotateTests
{
    [Fact]
    public void Execute_WhenCalled_RegistersMacroMoveAndMacroRotateDependencies()
    {
        Ioc.Clear();
        var registerCommand = new RegisterIoCDependencyMacroMoveRotate();
        registerCommand.Execute();

        var macroMove = Ioc.Resolve<ICommand>("Macro.Move");
        var macroRotate = Ioc.Resolve<ICommand>("Macro.Rotate");

        Assert.NotNull(macroMove);
        Assert.NotNull(macroRotate);
    }
}
