#nullable disable
using GameServer.Interfaces;
using GameServer.IoC;

namespace GameServer.Commands;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Rotate", (args) =>
        {
            var rotatingObject = Ioc.Resolve(args[0].ToString());
            return new RotateCommand((IRotatingObject)rotatingObject);
        });
    }
}
