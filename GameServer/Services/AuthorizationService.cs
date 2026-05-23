using GameServer.Interfaces;

namespace GameServer.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly Dictionary<string, HashSet<string>> _playerObjects = new();

    public void AssignObjectToPlayer(string playerId, string objectId)
    {
        if (!_playerObjects.ContainsKey(playerId))
            _playerObjects[playerId] = new HashSet<string>();
        
        _playerObjects[playerId].Add(objectId);
    }

    public bool IsAuthorized(string playerId, string objectId, string action)
    {
        return _playerObjects.ContainsKey(playerId) && 
               _playerObjects[playerId].Contains(objectId);
    }
}