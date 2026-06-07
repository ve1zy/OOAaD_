using Xunit;
using System.Collections.Generic;
using Moq;
using GameServer.Interfaces;
using GameServer.Commands;
using GameServer.IoC;
using GameServer.Models;

namespace GameServer.Tests
{
    [Collection("IoC Tests")]
    public class StartActionCommandTests
    {
        public StartActionCommandTests()
        {
            Ioc.Clear();
        }

        [Fact]
        public void Constructor_ShouldStoreOrder()
        {
            var order = new Dictionary<string, object>
            {
                ["action"] = "move",
                ["queue"] = new Queue<ICommand>()
            };

            var cmd = new StartActionCommand(order);
            Assert.NotNull(cmd);
        }

        [Fact]
        public void Execute_ShouldEnqueueInjectable_ToQueue()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();
            new RegisterIoCDependencyActionsStart().Execute();

            var queue = new Queue<ICommand>();
            var mockMovable = new Mock<IMovingObject>();
            var order = new Dictionary<string, object>
            {
                ["action"] = "move",
                ["queue"] = queue,
                ["movableObject"] = mockMovable.Object,
                ["position"] = new Vector(1, 2)
            };

            var cmd = new StartActionCommand(order);
            cmd.Execute();

            Assert.Single(queue);
            Assert.IsType<CommandInjectableCommand>(queue.Dequeue());
        }

        [Fact]
        public void Execute_ShouldStoreInjectableReferenceInOrder()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();
            new RegisterIoCDependencyActionsStart().Execute();

            var queue = new Queue<ICommand>();
            var mockMovable = new Mock<IMovingObject>();
            var order = new Dictionary<string, object>
            {
                ["action"] = "move",
                ["queue"] = queue,
                ["movableObject"] = mockMovable.Object,
                ["position"] = new Vector(1, 2)
            };

            var cmd = new StartActionCommand(order);
            cmd.Execute();

            Assert.True(order.ContainsKey("injectable"));
            Assert.IsType<CommandInjectableCommand>(order["injectable"]);
        }

        [Fact]
        public void Execute_ShouldResolveOperationWithCorrectAction()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();

            var moveMock = new Mock<ICommand>();
            Ioc.Register("Operations.move.Start", (Func<object[], object>)(args => moveMock.Object));

            var queue = new Queue<ICommand>();
            var order = new Dictionary<string, object>
            {
                ["action"] = "move",
                ["queue"] = queue
            };

            var cmd = new StartActionCommand(order);
            cmd.Execute();

            Assert.Single(queue);
            var enqueued = (CommandInjectableCommand)queue.Dequeue();
            enqueued.Execute();
            moveMock.Verify(m => m.Execute(), Times.Once);
        }

        [Fact]
        public void Execute_WithoutActionKey_ShouldThrowKeyNotFoundException()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();

            var order = new Dictionary<string, object>
            {
                ["queue"] = new Queue<ICommand>()
            };

            var cmd = new StartActionCommand(order);
            Assert.Throws<KeyNotFoundException>(() => cmd.Execute());
        }

        [Fact]
        public void Execute_WithoutQueueKey_ShouldThrowKeyNotFoundException()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();

            var order = new Dictionary<string, object>
            {
                ["action"] = "move"
            };

            var cmd = new StartActionCommand(order);
            Assert.Throws<KeyNotFoundException>(() => cmd.Execute());
        }
    }
}