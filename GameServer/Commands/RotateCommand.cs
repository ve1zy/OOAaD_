#nullable disable
using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class RotateCommand : ICommand
{
    private readonly IRotatingObject _rotatingObject;

    public RotateCommand(IRotatingObject rotatingObject)
    {
        _rotatingObject = rotatingObject;
    }

    public void Execute()
    {
        if (_rotatingObject == null)
        {
            throw new ArgumentException("Rotating object cannot be null");
        }

        var angle = _rotatingObject.Angle;
        var angularVelocity = _rotatingObject.AngularVelocity;

        if (angle == null)
        {
            throw new ArgumentException("Cannot determine angle of rotating object");
        }

        if (angularVelocity == null)
        {
            throw new ArgumentException("Cannot determine angular velocity of rotating object");
        }

        var newAngle = angle + angularVelocity;
        _rotatingObject.SetAngle(newAngle);
    }
}
