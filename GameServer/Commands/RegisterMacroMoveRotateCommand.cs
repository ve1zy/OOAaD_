#nullable disable
using GameServer.Interfaces;
using GameServer.IoC;
using GameServer.Strategies;

namespace GameServer.Commands;

public class RegisterMacroMoveRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Macro.Move", (args) =>
        {
            var strategy = new CreateMacroCommandStrategy("Move");
            return strategy.Resolve(args);
        });

        Ioc.Register("Macro.Rotate", (args) =>
        {
            var strategy = new CreateMacroCommandStrategy("Rotate");
            return strategy.Resolve(args);
        });
    }
}
