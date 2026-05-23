using GameServer.Models;

namespace GameServer.Interfaces;

public interface IMovingObject
{
    void Move(Vector position);
}
