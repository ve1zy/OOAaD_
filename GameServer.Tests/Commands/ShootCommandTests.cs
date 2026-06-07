using Xunit;
using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using GameServer.IoC;
using System;
using System.Collections.Generic;

namespace GameServer.Tests.Commands;

public class ShootCommandTests
{

    private class MockShooter : IShootingObject
    {
        public Vector Position { get; set; } = null!;
        public Vector Direction { get; set; } = null!;
    }

    private class MockRepository : IRepository
    {
        public Dictionary<string, object> Storage { get; } = new();
        public object GetById(string id) => Storage[id];
        public void Add(string id, object obj) => Storage[id] = obj;
        public void Delete(string id) => Storage.Remove(id);
    }

    private class MockCommand : ICommand
    {
        public bool IsExecuted { get; private set; }
        public void Execute() => IsExecuted = true;
    }

    [Fact]
    public void Execute_ShouldCreateTorpedo_AndStartItsMovement()
    {
        // Arrange
        Ioc.Clear();
        string shipId = "ship-777";
        
        var shooterMock = new MockShooter 
        { 
            Position = new Vector(10, 20), 
            Direction = new Vector(1, 0) 
        };
        var repoMock = new MockRepository();
        var startMoveCmdMock = new MockCommand();
        var dummyTorpedo = new object();

        // Настраиваем IoC окружение
        Ioc.Register("Adapters.IShootingObject", (args) => shooterMock);
        Ioc.Register("GameObjects.Torpedo", (args) => dummyTorpedo);
        Ioc.Register("Game.Repository", (args) => repoMock);
        Ioc.Register("Actions.Start", (args) => {
            var order = (Dictionary<string, object>)args[0];
            Assert.NotNull(order["ObjectId"]);
            Assert.Same(shooterMock.Direction, order["Velocity"]);
            return startMoveCmdMock;
        });

        var shootCommand = new ShootCommand(shipId);

        // Act
        shootCommand.Execute();

        // Assert
        Assert.Single(repoMock.Storage); // Торпеда добавлена в репозиторий
        Assert.Contains(dummyTorpedo, repoMock.Storage.Values);
        Assert.True(startMoveCmdMock.IsExecuted); // Команда на старт движения вызвана
    }

    [Fact]
    public void Execute_WithUndefinedPositionOrDirection_ShouldThrowInvalidOperationException()
    {
        // Arrange
        Ioc.Clear();
        var shooterMock = new MockShooter { Position = null!, Direction = new Vector(1, 1) };
        Ioc.Register("Adapters.IShootingObject", (args) => shooterMock);

        var shootCommand = new ShootCommand("ship-bad");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => shootCommand.Execute());
    }

    [Fact]
    public void Constructor_WithNullOrEmptyId_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ShootCommand(""));
        Assert.Throws<ArgumentException>(() => new ShootCommand(null!));
    }
}