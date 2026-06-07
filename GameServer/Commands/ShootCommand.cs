using System;
using System.Collections.Generic;
using GameServer.Interfaces;
using GameServer.IoC;

namespace GameServer.Commands;

public class ShootCommand : ICommand
{
    private readonly string _shootingObjectId;

    public ShootCommand(string shootingObjectId)
    {
        _shootingObjectId = string.IsNullOrEmpty(shootingObjectId)
            ? throw new ArgumentException("ID стреляющего объекта не может быть пустым.", nameof(shootingObjectId))
            : shootingObjectId;
    }

    public void Execute()
    {
        
        var shooter = (IShootingObject)Ioc.Resolve("Adapters.IShootingObject", _shootingObjectId);
        
        if (shooter.Position == null || shooter.Direction == null)
        {
            throw new InvalidOperationException("Невозможно произвести выстрел: позиция или направление объекта не определены.");
        }

      
        string torpedoId = Guid.NewGuid().ToString();

        
        var torpedo = Ioc.Resolve("GameObjects.Torpedo", shooter.Position, shooter.Direction);

        var repository = (IRepository)Ioc.Resolve("Game.Repository");
        repository.Add(torpedoId, torpedo);

        
        var startMoveOrder = new Dictionary<string, object>
        {
            { "ObjectId", torpedoId },
            { "Velocity", shooter.Direction }
        };

        var startMoveCmd = (ICommand)Ioc.Resolve("Actions.Start", startMoveOrder);
        startMoveCmd.Execute();
    }
}
