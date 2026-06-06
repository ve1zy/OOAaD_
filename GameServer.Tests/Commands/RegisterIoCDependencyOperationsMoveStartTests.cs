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
    public class RegisterIoCDependencyOperationsMoveStartTests
    {
        public RegisterIoCDependencyOperationsMoveStartTests()
        {
            Ioc.Clear();
        }

        [Fact]
        public void Execute_ShouldRegisterDependency_OperationsMoveStart()
        {
            new RegisterIoCDependencyOperationsMoveStart().Execute();

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
    }
}
