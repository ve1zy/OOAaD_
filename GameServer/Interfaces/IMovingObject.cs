using GameServer.Models;

namespace GameServer.Interfaces;

public interface IMovingObject
{
    Vector Position { get; set; }

    Vector Velocity { get; }
}
