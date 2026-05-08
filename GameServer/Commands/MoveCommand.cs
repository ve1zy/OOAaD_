using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class MoveCommand : ICommand
{
    private readonly IMovingObject _movableObject;

    public MoveCommand(IMovingObject movableObject)
    {
        _movableObject = movableObject;
    }

    public void Execute()
    {
        var newPosition = _movableObject.Position + _movableObject.Velocity;
        _movableObject.Position = newPosition;
    }
}
