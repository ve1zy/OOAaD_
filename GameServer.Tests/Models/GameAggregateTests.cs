using Xunit;
using GameServer.Models;
using GameServer.Interfaces;
using GameServer.IoC;
using System;

namespace GameServer.Tests.Models;

public class GameAggregateTests
{
    
    private class MockCommand : ICommand
    {
        public bool IsExecuted { get; private set; }
        public void Execute() => IsExecuted = true;
    }

    [Fact]
    public void HandleOrder_WithValidDataAndAuthorization_ShouldExecuteCommand()
    {
        // Arrange
        Ioc.Clear();
        var game = new Game("game-123");
        var mockCmd = new MockCommand();

        // Регистрируем успешную авторизацию
        Ioc.Register("Authorization.Check", (args) => true);
        
        // Регистрируем создание команды
        Ioc.Register("Commands.Shoot", (args) => {
            string targetObjectId = (string)args[0];
            Assert.Equal("ship-1", targetObjectId);
            return mockCmd;
        });

        // Act
        game.HandleOrder("player-1", "ship-1", "Shoot");

        // Assert
        Assert.True(mockCmd.IsExecuted);
    }

    [Fact]
    public void HandleOrder_WhenNotAuthorized_ShouldThrowUnauthorizedAccessException()
    {
        // Arrange
        Ioc.Clear();
        var game = new Game("game-123");

        // Симулируем отказ в авторизации
        Ioc.Register("Authorization.Check", (args) => false);

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => 
            game.HandleOrder("player-1", "ship-1", "Shoot"));
    }

    [Theory]
    [InlineData("", "ship-1", "Shoot")]
    [InlineData("player-1", "", "Shoot")]
    [InlineData("player-1", "ship-1", "")]
    public void HandleOrder_WithInvalidArgs_ShouldThrowArgumentException(string pId, string oId, string act)
    {
        // Arrange
        var game = new Game("game-123");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => game.HandleOrder(pId, oId, act));
    }

    [Fact]
    public void Constructor_WithNullOrEmptyId_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Game(""));
        Assert.Throws<ArgumentException>(() => new Game(null!));
    }
}