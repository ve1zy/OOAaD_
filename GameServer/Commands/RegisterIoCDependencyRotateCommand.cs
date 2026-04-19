using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Commands.Rotate", (args) =>
        {
            var rotatingObject = IoC.Ioc.Resolve(args[0].ToString());
            var angle = IoC.Ioc.Resolve(args[1].ToString());
            return new RotateCommand((IRotatingObject)rotatingObject, (Angle)angle);
        });
    }
}
