using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class RegisterIoCDependencyMacroMoveRotate : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Macro.Move", (args) =>
        {
            var commands = new ICommand[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                commands[i] = new MoveCommand((IMovingObject)IoC.Ioc.Resolve(args[i].ToString()), new Vector(0, 0, 0));
            }
            return new MacroCommand(commands);
        });
        
        IoC.Ioc.Register("Macro.Rotate", (args) =>
        {
            var commands = new ICommand[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                commands[i] = new RotateCommand((IRotatingObject)IoC.Ioc.Resolve(args[i].ToString()), new Angle(0));
            }
            return new MacroCommand(commands);
        });
    }
}
