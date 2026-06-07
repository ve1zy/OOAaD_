using GameServer.Interfaces;
using GameServer.Models;
using System;

namespace GameServer.Adapters;

public class TorpedoMovingAdapter : IMovingObject
{
    private readonly Torpedo _torpedo;

    public TorpedoMovingAdapter(Torpedo torpedo)
    {
        _torpedo = torpedo ?? throw new ArgumentNullException(nameof(torpedo));
    }

    
    public void Move(Vector newPosition)
    {
        if (newPosition == null) throw new ArgumentNullException(nameof(newPosition));
        _torpedo.Position = newPosition;
    }
}
