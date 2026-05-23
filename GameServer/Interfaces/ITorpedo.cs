namespace GameServer.Interfaces;

public interface ITorpedo
{
    string Id { get; }
    Vector Position { get; set; }
    Vector Velocity { get; set; }
    string OwnerId { get; }
}