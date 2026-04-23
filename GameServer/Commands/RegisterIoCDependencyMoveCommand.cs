#nullable disable

using GameServer.Interfaces;
using System;

namespace GameServer.Commands;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        GameServer.IoC.Ioc.Register("Commands.Move", (args) =>
        {
            if (args is null || args.Length < 2)
            {
                throw new ArgumentException("Commands.Move expects 2 arguments: movableObjectKey and positionKey");
            }

            var movableObjectKey = args[0] as string;
            var positionKey = args[1] as string;

            if (movableObjectKey is null || positionKey is null)
            {
                throw new ArgumentException("Commands.Move expects string keys");
            }

            var movableObject = GameServer.IoC.Ioc.Resolve(movableObjectKey);
            var position = GameServer.IoC.Ioc.Resolve(positionKey);
            return new MoveCommand((IMovingObject)movableObject, (GameServer.Models.Vector)position);
        });
    }
}
