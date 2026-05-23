#nullable enable
using GameServer.Interfaces;
using GameServer.Repositories;

namespace GameServer.Models;

public class Game
{
    private readonly IGameObjectsRepository _repository;

    public Game(IGameObjectsRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void Update()
    {
        // Update all moving objects (torpedoes)
        foreach (var torpedo in _repository.GetAll<ITorpedo>())
        {
            torpedo.Position = torpedo.Position + torpedo.Velocity;
        }
    }

    public T? GetObject<T>(string id) where T : class
    {
        return _repository.Get<T>(id);
    }
}