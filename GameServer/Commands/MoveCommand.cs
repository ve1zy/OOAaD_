using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class MoveCommand : ICommand
{
    private readonly IMovingObject _movableObject;
    private readonly Vector _position;
    private readonly Vector _velocity;

    public MoveCommand(IMovingObject movableObject, Vector position, Vector velocity)
    {
        _movableObject = movableObject;
        _position = position;
        _velocity = velocity;
    }

    public void Execute()
    {
        _movableObject.Move(_position, _velocity);
    }
}
