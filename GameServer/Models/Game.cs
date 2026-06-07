using System;
using GameServer.Interfaces;
using GameServer.IoC;

namespace GameServer.Models;

public class Game
{
    private readonly string _gameId;

    public Game(string gameId)
    {
        _gameId = string.IsNullOrEmpty(gameId) 
            ? throw new ArgumentException("ID игры не может быть пустым.", nameof(gameId)) 
            : gameId;
    }


    public void HandleOrder(string playerId, string objectId, string action, params object[] additionalArgs)
    {
        if (string.IsNullOrEmpty(playerId) || string.IsNullOrEmpty(objectId) || string.IsNullOrEmpty(action))
        {
            throw new ArgumentException("Идентификаторы игрока, объекта и действия не могут быть пустыми.");
        }

        
        bool isAuthorized = (bool)Ioc.Resolve("Authorization.Check", playerId, objectId, action);
        
        if (!isAuthorized)
        {
            throw new UnauthorizedAccessException($"Игрок '{playerId}' не авторизован для действия '{action}' с объектом '{objectId}'.");
        }

        
        var commandArgs = new object[additionalArgs.Length + 1];
        commandArgs[0] = objectId;
        Array.Copy(additionalArgs, 0, commandArgs, 1, additionalArgs.Length);

        
        var command = (ICommand)Ioc.Resolve($"Commands.{action}", commandArgs);

       
        command.Execute();
    }
}
