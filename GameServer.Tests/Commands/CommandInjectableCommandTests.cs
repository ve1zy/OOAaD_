using Xunit;
using Moq;
using GameServer.Interfaces;
using SpaceGame.Commands;

namespace SpaceGame.Tests
{
    [Collection("IoC Tests")]
    public class CommandInjectableCommandTests
    {
        [Fact]
        public void Execute_ShouldCallInjectedCommand_WhenCommandWasInjected()
        {
            var mockCommand = new Mock<ICommand>();
            var injectable = new CommandInjectableCommand();
            injectable.Inject(mockCommand.Object);

            injectable.Execute();

            mockCommand.Verify(c => c.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowException_WhenCommandWasNotInjected()
        {
            var injectable = new CommandInjectableCommand();

            var exception = Assert.Throws<InvalidOperationException>(() => injectable.Execute());
            Assert.Contains("not injected", exception.Message);
        }

        [Fact]
        public void Inject_ShouldThrowException_WhenCommandIsNull()
        {
            var injectable = new CommandInjectableCommand();

            Assert.Throws<ArgumentNullException>(() => injectable.Inject(null!));
        }
    }
}
