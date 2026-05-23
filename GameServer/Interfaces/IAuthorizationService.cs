namespace GameServer.Interfaces;

public interface IAuthorizationService
{
    bool IsAuthorized(string playerId, string objectId, string action);
}