using Xunit;
using System.Collections.Generic;
using Moq;
using GameServer.Interfaces;
using GameServer.Commands;
using GameServer.IoC;

namespace GameServer.Tests
{
    [Collection("IoC Tests")]
    public class RegisterIoCDependencyActionsStopTests
    {
        public RegisterIoCDependencyActionsStopTests()
        {
            Ioc.Clear();
        }

        [Fact]
        public void Execute_ShouldRegisterDependency_ActionsStop()
        {
            new RegisterIoCDependencyActionsStop().Execute();

            var mockInjectable = new Mock<CommandInjectableCommand>();
            var order = new Dictionary<string, object>
            {
                ["injectable"] = mockInjectable.Object
            };

            var result = Ioc.Resolve("Actions.Stop", order);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ICommand>(result);
        }

        [Fact]
        public void Resolve_ActionsStop_ShouldReturnStopActionCommand()
        {
            new RegisterIoCDependencyActionsStop().Execute();

            var order = new Dictionary<string, object>
            {
                ["injectable"] = new Mock<CommandInjectableCommand>().Object
            };

            var result = Ioc.Resolve("Actions.Stop", order);
            Assert.IsType<StopActionCommand>(result);
        }
    }
}