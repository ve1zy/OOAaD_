using Xunit;
using Moq;
using GameServer.Interfaces;

namespace GameServer.Tests.Interfaces
{
    public class ICommandTests
    {
        [Fact]
        public void Execute_UsingMock_CallsMethodSuccessfully()
        {
            // Arrange
            var mockCommand = new Mock<ICommand>();
            bool wasExecuted = false;
            mockCommand.Setup(cmd => cmd.Execute()).Callback(() => wasExecuted = true);

            // Act
            mockCommand.Object.Execute();

            // Assert
            Assert.True(wasExecuted);
            mockCommand.Verify(cmd => cmd.Execute(), Times.Once);
        }
    }
}