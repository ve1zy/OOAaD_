using GameServer.IoC;
using GameServer.Strategies;

namespace GameServer.Commands;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        Ioc.Register("Macro.Move", args => CreateMacroCommandStrategy.Resolve("Macro.Move"));
        Ioc.Register("Macro.Rotate", args => CreateMacroCommandStrategy.Resolve("Macro.Rotate"));
    }
}
