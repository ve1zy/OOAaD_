using System.Collections.Concurrent;
using GameServer.Interfaces;

namespace GameServer.Services;

public class AuthorizationService : IAuthorizationService
{
    
    private readonly ConcurrentDictionary<string, HashSet<string>> _permissions = new();

    public void GrantPermission(string playerId, string objectId, string action)
    {
        if (string.IsNullOrEmpty(playerId) || string.IsNullOrEmpty(objectId) || string.IsNullOrEmpty(action))
        {
            throw new ArgumentException("Параметры авторизации не могут быть пустыми.");
        }

        string key = $"{playerId}:{objectId}";
        _permissions.AddOrUpdate(
            key,
            _ => new HashSet<string> { action },
            (_, actions) => { actions.Add(action); return actions; }
        );
    }

    public bool IsAuthorized(string playerId, string objectId, string action)
    {
        if (string.IsNullOrEmpty(playerId) || string.IsNullOrEmpty(objectId) || string.IsNullOrEmpty(action))
        {
            return false;
        }

        string key = $"{playerId}:{objectId}";
        return _permissions.TryGetValue(key, out var actions) && actions.Contains(action);
    }
}
