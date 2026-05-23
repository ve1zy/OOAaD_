using GameServer.Interfaces;
using GameServer.Models;
using GameServer.Repositories;

namespace GameServer.Commands;

public class ShootCommand : ICommand
{
    private readonly IShip _ship;
    private readonly IGameObjectsRepository _repository;
    private readonly string _torpedoId;

    public ShootCommand(IShip ship, IGameObjectsRepository repository, string torpedoId = "")
    {
        _ship = ship ?? throw new ArgumentNullException(nameof(ship));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _torpedoId = string.IsNullOrEmpty(torpedoId) ? Guid.NewGuid().ToString() : torpedoId;
    }

    public void Execute()
    {
        if (!_ship.CanShoot)
            throw new InvalidOperationException("Ship cannot shoot");

        // Calculate torpedo velocity based on ship direction
        // Torpedo velocity = ship velocity + direction-based speed
        var torpedoVelocity = _ship.Velocity + new Vector(10, 0); // Simplified: torpedo speed in direction
        
        var torpedo = new Torpedo(_torpedoId, _ship.Position, torpedoVelocity, _ship.Id);
        _repository.Add(_torpedoId, torpedo);
    }
}