using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class MoveCommand : ICommand
{
    private readonly IMovingObject _movableObject;
    private readonly Vector _position;

    public MoveCommand(IMovingObject movableObject, Vector position)
    {
        _movableObject = movableObject;
        _position = position;
    }

    public void Execute()
    {
        _movableObject.Move(_position);
    }
}
