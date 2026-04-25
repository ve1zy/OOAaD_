using GameServer.Models;

namespace GameServer.Interfaces;

public interface IMovingObject
{
    void Move(GameServer.Models.Vector position);
}
