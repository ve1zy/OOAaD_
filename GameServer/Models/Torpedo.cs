using GameServer.Interfaces;

namespace GameServer.Models;

public class Torpedo : ITorpedo
{
    public string Id { get; }
    public Vector Position { get; set; }
    public Vector Velocity { get; set; }
    public string OwnerId { get; }

    public Torpedo(string id, Vector position, Vector velocity, string ownerId)
    {
        Id = id;
        Position = position;
        Velocity = velocity;
        OwnerId = ownerId;
    }
}