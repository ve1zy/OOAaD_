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
        try
        {
            var newAngle = _rotatingObject.Angle + _rotatingObject.AngularVelocity;
            _rotatingObject.SetAngle(newAngle);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to rotate object.", ex);
        }
    }
}
