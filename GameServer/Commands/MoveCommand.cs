using GameServer.Interfaces;
using GameServer.Models;

namespace GameServer.Commands;

public class MoveCommand : ICommand
{
    private readonly IMovingObject _movableObject;
    private readonly GameServer.Models.Vector _position;

    public MoveCommand(IMovingObject movableObject, GameServer.Models.Vector position)
    {
        _movableObject = movableObject;
        _position = position;
    }

    public void Execute()
    {
        _movableObject.Move(_position);
    }
}
