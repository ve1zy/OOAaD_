using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class MoveCommand : ICommand
{
    private readonly IMovingObject _movingObject;

    public MoveCommand(IMovingObject movingObject)
    {
        _movingObject = movingObject ?? throw new ArgumentNullException(nameof(movingObject));
    }

    public void Execute()
    {
        try
        {
            var newPosition = _movingObject.Position + _movingObject.Velocity;
            _movingObject.SetPosition(newPosition);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to move object.", ex);
        }
    }
}
