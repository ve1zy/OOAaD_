using Xunit;
using GameServer.Services;
using System;

namespace GameServer.Tests.Services;

public class AuthorizationTests
{
    [Fact]
    public void IsAuthorized_WithValidPermission_ShouldReturnTrue()
    {
        // Arrange
        var authService = new AuthorizationService();
        string playerId = "player-1";
        string objectId = "ship-1";
        string action = "Shoot";

        authService.GrantPermission(playerId, objectId, action);

        // Act
        bool result = authService.IsAuthorized(playerId, objectId, action);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAuthorized_WithInvalidPermission_ShouldReturnFalse()
    {
        // Arrange
        var authService = new AuthorizationService();
        authService.GrantPermission("player-1", "ship-1", "Shoot");

        // Act & Assert
        Assert.False(authService.IsAuthorized("player-2", "ship-1", "Shoot")); // Другой игрок
        Assert.False(authService.IsAuthorized("player-1", "ship-2", "Shoot")); // Другой корабль
        Assert.False(authService.IsAuthorized("player-1", "ship-1", "Move"));  // Другое действие
    }

    [Theory]
    [InlineData("", "ship-1", "Shoot")]
    [InlineData("player-1", "", "Shoot")]
    [InlineData("player-1", "ship-1", "")]
    [InlineData(null, "ship-1", "Shoot")]
    public void IsAuthorized_WithNullOrEmptyArgs_ShouldReturnFalse(string pId, string oId, string act)
    {
        // Arrange
        var authService = new AuthorizationService();

        // Act
        bool result = authService.IsAuthorized(pId, oId, act);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GrantPermission_WithInvalidArgs_ShouldThrowArgumentException()
    {
        // Arrange
        var authService = new AuthorizationService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => authService.GrantPermission("", "ship-1", "Shoot"));
    }
}