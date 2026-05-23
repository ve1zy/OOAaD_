// RegisterIoCDependencyActionsStopTests.cs
// ЗАДАНИЕ 20: Тесты под IoC с Resolve(string) -> object
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
            new RegisterDependencyCommandInjectableCommand().Execute();
            new RegisterIoCDependencyActionsStop().Execute();

            var mockInjectable = new Mock<ICommandInjectable>();
            var order = new Dictionary<string, object>
            {
                ["command"] = mockInjectable.Object
            };

            var result = Ioc.Resolve("Actions.Stop", order);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ICommand>(result);
        }

        [Fact]
        public void Execute_ShouldInjectEmptyCommand_ForConstantTimeStop()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();
            new RegisterIoCDependencyActionsStop().Execute();

            var injectable = new CommandInjectableCommand();
            var realCommand = new Mock<ICommand>();
            injectable.Inject(realCommand.Object);

            var order = new Dictionary<string, object>
            {
                ["command"] = injectable
            };

            Ioc.Resolve("Actions.Stop", order);

            injectable.Execute();
            realCommand.Verify(c => c.Execute(), Times.Never);
        }

        [Fact]
        public void Stop_ShouldReplaceCommand_WithEmptyCommand()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();
            new RegisterIoCDependencyActionsStop().Execute();

            var injectable = (CommandInjectableCommand)Ioc.Resolve("Commands.CommandInjectable");
            var originalCommand = new Mock<ICommand>();
            injectable.Inject(originalCommand.Object);

            var order = new Dictionary<string, object>
            {
                ["command"] = injectable
            };

            var stopCommand = Ioc.Resolve("Actions.Stop", order);
            Assert.IsAssignableFrom<ICommand>(stopCommand);

            injectable.Execute();
            originalCommand.Verify(c => c.Execute(), Times.Never);
        }
    }
}
