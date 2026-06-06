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
    public class RegisterIoCDependencyOperationsRotateStartTests
    {
        public RegisterIoCDependencyOperationsRotateStartTests()
        {
            Ioc.Clear();
        }

        [Fact]
        public void Execute_ShouldRegisterDependency_OperationsRotateStart()
        {
            new RegisterIoCDependencyOperationsRotateStart().Execute();

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
