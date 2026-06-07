using Xunit;
using GameServer.Models;
using GameServer.Interfaces;
using GameServer.Commands;
using GameServer.Repositories;
using GameServer.Adapters;
using GameServer.IoC;
using System;
using System.Collections.Generic;

namespace GameServer.Tests.Integration;

public class TorpedoMovementIntegrationTests
{
    
    private class RealMoveCommand : ICommand
    {
        private readonly IMovingObject _movingObject;
        private readonly Vector _velocity;

        public RealMoveCommand(IMovingObject movingObject, Vector velocity)
        {
            _movingObject = movingObject;
            _velocity = velocity;
        }

        public void Execute()
        {

            var currentPosition = (Vector)Ioc.Resolve("GameObjects.GetTorpedoPosition");
            var newPosition = currentPosition + _velocity;
            _movingObject.Move(newPosition);
        }
    }

    [Fact]
    public void FullShootAndMoveWorkflow_ShouldWorkEndToEnd()
    {
        
        Ioc.Clear();
        var repository = new GameRepository();
        var game = new Game("game-42");

        string playerId = "player-pasha";
        string shipId = "player-ship";
        var shipPosition = new Vector(10, 10);
        var shipDirection = new Vector(2, 3); 

        
        var mockShooter = new Moq.Mock<IShootingObject>();
        mockShooter.Setup(s => s.Position).Returns(shipPosition);
        mockShooter.Setup(s => s.Direction).Returns(shipDirection);

        
        Ioc.Register("Game.Repository", (args) => repository);
        Ioc.Register("Authorization.Check", (args) => true); // Авторизация успешна
        Ioc.Register("Adapters.IShootingObject", (args) => mockShooter.Object);

        
        Ioc.Register("GameObjects.Torpedo", (args) => new Torpedo((Vector)args[0], (Vector)args[1]));

        
        string createdTorpedoId = null!;

        
        Ioc.Register("Actions.Start", (args) =>
        {
            var order = (Dictionary<string, object>)args[0];
            createdTorpedoId = (string)order["ObjectId"];
            var velocity = (Vector)order["Velocity"];

            
            var rawTorpedo = (Torpedo)repository.GetById(createdTorpedoId);
            
            
            var movingAdapter = new TorpedoMovingAdapter(rawTorpedo);

            
            Ioc.Register("GameObjects.GetTorpedoPosition", (a) => rawTorpedo.Position);

            
            return new RealMoveCommand(movingAdapter, velocity);
        });

       
        Ioc.Register("Commands.Shoot", (args) => new ShootCommand((string)args[0]));

        //  Act 
        game.HandleOrder(playerId, shipId, "Shoot");

        // Assert 
        Assert.NotNull(createdTorpedoId); 
        

        var torpedoInDb = (Torpedo)repository.GetById(createdTorpedoId);
        Assert.NotNull(torpedoInDb);


        Assert.Equal(12, torpedoInDb.Position[0]);
        Assert.Equal(13, torpedoInDb.Position[1]);
    }
}
