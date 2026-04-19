using GameServer.Models;

namespace GameServer.Interfaces;

public interface IRotatingObject
{
    Angle Angle { get; }
    Angle AngularVelocity { get; }
    void SetAngle(Angle newAngle);
}
