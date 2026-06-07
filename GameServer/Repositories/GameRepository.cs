using System.Collections.Concurrent;
using GameServer.Interfaces;

namespace GameServer.Repositories;

public class GameRepository : IRepository
{
    private readonly ConcurrentDictionary<string, object> _storage = new();

    public object GetById(string id)
    {
        if (_storage.TryGetValue(id, out var obj))
        {
            return obj;
        }
        throw new KeyNotFoundException($"Игровой объект с ID '{id}' не найден.");
    }

    public void Add(string id, object obj)
    {
        if (string.IsNullOrEmpty(id) || obj == null)
        {
            throw new ArgumentNullException(id == null ? nameof(id) : nameof(obj));
        }
        _storage[id] = obj;
    }

    public void Delete(string id)
    {
        if (!_storage.TryRemove(id, out _))
        {
            throw new KeyNotFoundException($"Не удалось удалить: объект с ID '{id}' не найден.");
        }
    }
}