using GameServer.Models;

namespace GameServer.Interfaces;

public interface IMovingObject
{
    Vector Position { get; }
    Vector Velocity { get; }
    void SetPosition(Vector newPosition);
}
