#nullable enable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Commands.Move", (args) =>
        {
            var key = args[0]?.ToString() ?? throw new ArgumentException("Key cannot be null");
            var movableObject = IoC.Ioc.Resolve(key);
            return new MoveCommand((IMovingObject)movableObject);
        });
    }
}
