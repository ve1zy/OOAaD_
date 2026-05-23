using GameServer.Models;

namespace GameServer.Interfaces;

public interface IShip
{
    string Id { get; }
    Vector Position { get; set; }
    Vector Velocity { get; set; }
    Angle Direction { get; set; }
    bool CanShoot { get; }
}