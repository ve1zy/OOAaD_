#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Commands.Rotate", (args) =>
        {
            var rotatingObject = IoC.Ioc.Resolve(args[0].ToString());
            return new RotateCommand((IRotatingObject)rotatingObject);
        });
    }
}
