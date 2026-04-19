using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class RotateCommand : ICommand
{
    private readonly IRotatingObject _rotatingObject;
    private readonly Angle _angle;

    public RotateCommand(IRotatingObject rotatingObject, Angle angle)
    {
        _rotatingObject = rotatingObject;
        _angle = angle;
    }

    public void Execute()
    {
        _rotatingObject.Rotate(_angle);
    }
}
