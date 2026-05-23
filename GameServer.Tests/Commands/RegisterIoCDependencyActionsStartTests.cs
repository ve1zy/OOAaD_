// RegisterIoCDependencyActionsStartTests.cs
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

            Ioc.Resolve("Actions.Start", order);

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

            // Angle — отдельный тип, не Vector!
            mockRotating.Setup(r => r.Angle).Returns(new Angle(0, 1));
            mockRotating.Setup(r => r.AngularVelocity).Returns(new Angle(1, 4));

            var order = new Dictionary<string, object>
            {
                ["action"] = "rotate",
                ["queue"] = queue,
                ["rotatingObject"] = mockRotating.Object
            };

            Ioc.Resolve("Actions.Start", order);

            Assert.Single(queue);
            var enqueued = queue.Dequeue();
            Assert.IsType<CommandInjectableCommand>(enqueued);
        }
    }
}
