using GameServer.Interfaces;
using GameServer.IoC;
using GameServer.Models;

namespace GameServer.Commands;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Move", (args) =>
        {
            var movableObject = (IMovingObject)Ioc.Resolve("Adapters.IMovingObject", args[0]);
            var position = (Vector)Ioc.Resolve(args[1].ToString());
            return new MoveCommand(movableObject, position);
        });
    }
}
