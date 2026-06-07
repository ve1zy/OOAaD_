using Xunit;
using GameServer.Repositories;
using System;
using System.Collections.Generic;

namespace GameServer.Tests.Repositories;

public class GameRepositoryTests
{
    [Fact]
    public void AddAndGet_ShouldReturnCorrectObject()
    {
        // Arrange
        var repository = new GameRepository();
        var testObject = new object();
        string id = "ship-1";

        // Act
        repository.Add(id, testObject);
        var result = repository.GetById(id);

        // Assert
        Assert.Same(testObject, result);
    }

    [Fact]
    public void GetById_WithMissingId_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var repository = new GameRepository();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => repository.GetById("non-existent"));
    }

    [Fact]
    public void Add_WithNullArguments_ShouldThrowArgumentNullException()
    {
        // Arrange
        var repository = new GameRepository();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => repository.Add(null!, new object()));
        Assert.Throws<ArgumentNullException>(() => repository.Add("id", null!));
    }

    [Fact]
    public void Delete_ExistingObject_ShouldRemoveIt()
    {
        // Arrange
        var repository = new GameRepository();
        var testObject = new object();
        string id = "torpedo-ex";
        repository.Add(id, testObject);

        // Act
        repository.Delete(id);

        // Assert
        Assert.Throws<KeyNotFoundException>(() => repository.GetById(id));
    }

    [Fact]
    public void Delete_NonExistingObject_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var repository = new GameRepository();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => repository.Delete("ghost-id"));
    }
}