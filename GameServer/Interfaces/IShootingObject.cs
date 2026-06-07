using GameServer.Models;

namespace GameServer.Interfaces;

public interface IShootingObject
{
    Vector Position { get; }
    Vector Direction { get; } 
}
