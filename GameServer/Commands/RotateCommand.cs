#nullable disable
using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class RotateCommand : ICommand
{
    private readonly IRotatingObject _rotatingObject;

    public RotateCommand(IRotatingObject rotatingObject)
    {
        _rotatingObject = rotatingObject ?? throw new ArgumentNullException(nameof(rotatingObject));
    }

    public void Execute()
    {
        var angle = _rotatingObject.Angle;
        var angularVelocity = _rotatingObject.AngularVelocity;

        var newAngle = angle + angularVelocity;
        _rotatingObject.SetAngle(newAngle);
    }
}
