using GameServer.IoC;
using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        Ioc.Register("Specs.Move", new[] { "Commands.Move" });
        Ioc.Register("Specs.Rotate", new[] { "Commands.Rotate" });
    }
}
