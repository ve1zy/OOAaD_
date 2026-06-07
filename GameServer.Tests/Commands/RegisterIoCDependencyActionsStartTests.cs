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
        public void Execute_ShouldRegisterOperationsMoveStart()
        {
            new RegisterIoCDependencyActionsStart().Execute();

            var mockMovable = new Mock<IMovingObject>();
            var order = new Dictionary<string, object>
            {
                ["movableObject"] = mockMovable.Object,
                ["position"] = new Vector(3, 4)
            };

            var result = Ioc.Resolve("Operations.move.Start", order);
            Assert.NotNull(result);
            Assert.IsType<MoveCommand>(result);
        }

        [Fact]
        public void Execute_ShouldRegisterOperationsRotateStart()
        {
            new RegisterIoCDependencyActionsStart().Execute();

            var mockRotating = new Mock<IRotatingObject>();
            mockRotating.Setup(r => r.Angle).Returns(new Angle(0, 1));
            mockRotating.Setup(r => r.AngularVelocity).Returns(new Angle(1, 4));

            var order = new Dictionary<string, object>
            {
                ["rotatingObject"] = mockRotating.Object
            };

            var result = Ioc.Resolve("Operations.rotate.Start", order);
            Assert.NotNull(result);
            Assert.IsType<RotateCommand>(result);
        }
    }
}