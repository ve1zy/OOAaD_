#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterMacroMoveRotateCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Macro.Move", (args) =>
        {
            var movableObjectKey = args[0].ToString();
            var positionKey = args[1].ToString();
            var moveCommandKey = $"Commands.Move.{movableObjectKey}";
            IoC.Ioc.Register(moveCommandKey, (innerArgs) =>
            {
                var movableObject = IoC.Ioc.Resolve(movableObjectKey);
                var position = IoC.Ioc.Resolve(positionKey);
                return new MoveCommand((IMovingObject)movableObject, (GameServer.Models.Vector)position);
            });
            return new MacroCommand(new ICommand[] { (ICommand)IoC.Ioc.Resolve(moveCommandKey) });
        });

        IoC.Ioc.Register("Macro.Rotate", (args) =>
        {
            var rotatingObjectKey = args[0].ToString();
            var rotateCommandKey = $"Commands.Rotate.{rotatingObjectKey}";
            IoC.Ioc.Register(rotateCommandKey, (innerArgs) =>
            {
                var rotatingObject = IoC.Ioc.Resolve(rotatingObjectKey);
                return new RotateCommand((IRotatingObject)rotatingObject);
            });
            return new MacroCommand(new ICommand[] { (ICommand)IoC.Ioc.Resolve(rotateCommandKey) });
        });
    }
}
