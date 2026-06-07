using System;
using GameServer.Models;

namespace GameServer.Models;

public class Torpedo
{
   
    public Vector Position { get; set; }
    public Vector Velocity { get; set; }

    public Torpedo(Vector position, Vector velocity)
    {
        Position = position ?? throw new ArgumentNullException(nameof(position));
        Velocity = velocity ?? throw new ArgumentNullException(nameof(velocity));
    }
}
