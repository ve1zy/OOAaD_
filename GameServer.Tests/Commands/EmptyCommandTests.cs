using Xunit;
using GameServer.Commands;

namespace GameServer.Tests.Commands;

public class EmptyCommandTests
{
    [Fact]
    public void Execute_DoesNothingAndDoesNotThrow()
    {
        // Arrange
        var command = new EmptyCommand();
        
        // Act & Assert
        var ex = Record.Exception(() => command.Execute());
        Assert.Null(ex); 
    }
}