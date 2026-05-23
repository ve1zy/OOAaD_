using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.Models;
using GameServer.Repositories;
using Moq;
using Xunit;

namespace GameServer.Tests.Commands;

public class ShootCommandTests
{
    [Fact]
    public void ShootCommand_CreatesTorpedo_WhenShipCanShoot()
    {
        // Arrange
        var ship = new Mock<IShip>();
        ship.Setup(s => s.Id).Returns("ship1");
        ship.Setup(s => s.Position).Returns(new Vector(10, 20));
        ship.Setup(s => s.Velocity).Returns(new Vector(1, 0));
        ship.Setup(s => s.CanShoot).Returns(true);
        
        var repository = new GameObjectsRepository();
        
        var command = new ShootCommand(ship.Object, repository, "test-torpedo");
        
        // Act
        command.Execute();
        
        // Assert
        var torpedo = repository.Get<ITorpedo>("test-torpedo");
        Assert.NotNull(torpedo);
    }

    [Fact]
    public void ShootCommand_ThrowsException_WhenShipCannotShoot()
    {
        // Arrange
        var ship = new Mock<IShip>();
        ship.Setup(s => s.CanShoot).Returns(false);
        
        var repository = new GameObjectsRepository();
        
        var command = new ShootCommand(ship.Object, repository);
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }
}