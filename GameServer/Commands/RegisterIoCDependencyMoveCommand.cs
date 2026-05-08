using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Commands.Move", (args) =>
        {
            var movableObject = IoC.Ioc.Resolve(args[0].ToString());
            return new MoveCommand((IMovingObject)movableObject);
        });
    }
}
