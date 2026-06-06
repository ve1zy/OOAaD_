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
    public class RegisterIoCDependencyActionsStartTests
    {
        public RegisterIoCDependencyActionsStartTests()
        {
            Ioc.Clear();
        }

        [Fact]
        public void Execute_ShouldRegisterDependency_ActionsStart()
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

            var result = Ioc.Resolve("Actions.Start", order);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ICommand>(result);
        }

        [Fact]
        public void Resolve_ActionsStart_ShouldReturnStartActionCommand()
        {
            new RegisterIoCDependencyActionsStart().Execute();

            var order = new Dictionary<string, object>
            {
                ["action"] = "move",
                ["queue"] = new Queue<ICommand>()
            };

            var result = Ioc.Resolve("Actions.Start", order);
            Assert.IsType<StartActionCommand>(result);
        }

        [Fact]
        public void Execute_ShouldEnqueueMoveCommand_ToQueue()
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

            var cmd = (ICommand)Ioc.Resolve("Actions.Start", order);
            cmd.Execute();

            Assert.Single(queue);
            var enqueued = queue.Dequeue();
            Assert.IsType<CommandInjectableCommand>(enqueued);
        }

        [Fact]
        public void Execute_ShouldEnqueueRotateCommand_ToQueue()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();
            new RegisterIoCDependencyActionsStart().Execute();

            var queue = new Queue<ICommand>();
            var mockRotating = new Mock<IRotatingObject>();
            mockRotating.Setup(r => r.Angle).Returns(new Angle(0, 1));
            mockRotating.Setup(r => r.AngularVelocity).Returns(new Angle(1, 4));

            var order = new Dictionary<string, object>
            {
                ["action"] = "rotate",
                ["queue"] = queue,
                ["rotatingObject"] = mockRotating.Object
            };

            var cmd = (ICommand)Ioc.Resolve("Actions.Start", order);
            cmd.Execute();

            Assert.Single(queue);
            var enqueued = queue.Dequeue();
            Assert.IsType<CommandInjectableCommand>(enqueued);
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

            var cmd = (ICommand)Ioc.Resolve("Actions.Start", order);
            cmd.Execute();

            Assert.True(order.ContainsKey("injectable"));
            Assert.IsType<CommandInjectableCommand>(order["injectable"]);
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
    }
}
