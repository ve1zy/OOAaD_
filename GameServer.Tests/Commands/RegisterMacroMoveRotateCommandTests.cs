#nullable disable
using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterMacroMoveRotateCommandTests
{
    [Fact]
    public void Execute_RegistersMacroMoveAndMacroRotate()
    {
        Ioc.Clear();

        var mockMove = new MockCommand();
        var mockRotate = new MockCommand();

        Ioc.Register("Specs.Move", new string[] { "Commands.Move" });
        Ioc.Register("Specs.Rotate", new string[] { "Commands.Rotate" });
        Ioc.Register("Commands.Move", mockMove);
        Ioc.Register("Commands.Rotate", mockRotate);

        var registerCommand = new RegisterMacroMoveRotateCommand();
        registerCommand.Execute();

        var macroMove = (MacroCommand)Ioc.Resolve("Macro.Move");
        var macroRotate = (MacroCommand)Ioc.Resolve("Macro.Rotate");

        Assert.NotNull(macroMove);
        Assert.NotNull(macroRotate);

        macroMove.Execute();
        Assert.True(mockMove.Executed);

        macroRotate.Execute();
        Assert.True(mockRotate.Executed);
    }

    private class MockCommand : ICommand
    {
        public bool Executed { get; private set; }

        public void Execute()
        {
            Executed = true;
        }
    }
}
