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
            var position = (Vector)Ioc.Resolve("Position", args[0]);
            var velocity = (Vector)Ioc.Resolve("Velocity", args[0]);
            return new MoveCommand(movableObject, position, velocity);
        });
    }
}
