using GameServer.Models;
using GameServer.Repositories;
using Xunit;

namespace GameServer.Tests.Repositories;

public class GameObjectsRepositoryTests
{
    [Fact]
    public void Repository_AddAndGet_ReturnsObject()
    {
        // Arrange
        var repository = new GameObjectsRepository();
        var vector = new Vector(10, 20);
        
        // Act
        repository.Add("test", vector);
        var result = repository.Get<Vector>("test");
        
        // Assert
        Assert.Equal(vector, result);
    }

    [Fact]
    public void Repository_GetAll_ReturnsAllObjects()
    {
        // Arrange
        var repository = new GameObjectsRepository();
        repository.Add("v1", new Vector(1, 2));
        repository.Add("v2", new Vector(3, 4));
        
        // Act
        var all = repository.GetAll<Vector>().ToList();
        
        // Assert
        Assert.Equal(2, all.Count);
    }
}