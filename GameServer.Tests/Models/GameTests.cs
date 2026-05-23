#nullable enable
using GameServer.Interfaces;
using GameServer.Models;
using GameServer.Repositories;
using Xunit;

namespace GameServer.Tests.Models;

public class GameTests
{
    [Fact]
    public void Game_Update_MovesTorpedoes()
    {
        // Arrange
        var repository = new GameObjectsRepository();
        var torpedo = new Torpedo("t1", new Vector(0, 0), new Vector(1, 1), "ship1");
        repository.Add("t1", torpedo);
        
        var game = new Game(repository);
        
        // Act
        game.Update();
        
        // Assert
        var updatedTorpedo = repository.Get<ITorpedo>("t1");
        Assert.NotNull(updatedTorpedo);
        Assert.Equal(new Vector(1, 1), updatedTorpedo.Position);
    }
}