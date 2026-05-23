using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using GameServer.Repositories;
using Xunit;

namespace GameServer.Tests.Commands;

public class ShootCommandTests
{
    [Fact]
    public void ShootCommand_CreatesTorpedo_WhenShipCanShoot()
    {
        // Arrange
        var ship = new MockShip { Id = "ship1", Position = new Vector(10, 20), Velocity = new Vector(1, 0), CanShoot = true };
        var repository = new GameObjectsRepository();
        
        var command = new ShootCommand(ship, repository, "test-torpedo");
        
        // Act
        command.Execute();
        
        // Assert
        var torpedo = repository.Get<ITorpedo>("test-torpedo");
        Assert.NotNull(torpedo);
    }

    [Fact]
    public void ShootCommand_CreatesTorpedo_WithGeneratedId_WhenIdNotProvided()
    {
        // Arrange
        var ship = new MockShip { Id = "ship1", Position = new Vector(10, 20), Velocity = new Vector(1, 0), CanShoot = true };
        var repository = new GameObjectsRepository();
        
        var command = new ShootCommand(ship, repository);
        
        // Act
        command.Execute();
        
        // Assert - check that a torpedo was added (we don't know the generated ID)
        var allTorpedoes = repository.GetAll<ITorpedo>().ToList();
        Assert.Single(allTorpedoes);
    }

    [Fact]
    public void ShootCommand_ThrowsException_WhenShipCannotShoot()
    {
        // Arrange
        var ship = new MockShip { CanShoot = false };
        var repository = new GameObjectsRepository();
        
        var command = new ShootCommand(ship, repository);
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    private class MockShip : IShip
    {
        public string Id { get; set; } = "";
        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        public Angle Direction { get; set; }
        public bool CanShoot { get; set; }
    }
}