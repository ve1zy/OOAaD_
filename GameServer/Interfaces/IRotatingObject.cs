using GameServer.Models;

namespace GameServer.Interfaces;

public interface IRotatingObject
{
    void Rotate(Angle angle);
}
