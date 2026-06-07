using Xunit;
using GameServer.Interfaces;
using GameServer.Commands;
using GameServer.IoC;

namespace GameServer.Tests
{
    [Collection("IoC Tests")]
    public class RegisterDependencyCommandInjectableCommandTests
    {
        public RegisterDependencyCommandInjectableCommandTests()
        {
            Ioc.Clear();
        }

        [Fact]
        public void Execute_ShouldRegisterDependency_ResolvableAsICommand()
        {
            var registerCmd = new RegisterDependencyCommandInjectableCommand();
            registerCmd.Execute();

            var result = Ioc.Resolve("Commands.CommandInjectable");
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ICommand>(result);
            Assert.IsType<CommandInjectableCommand>(result);
        }

        [Fact]
        public void Execute_ShouldRegisterDependency_ResolvableAsICommandInjectable()
        {
            var registerCmd = new RegisterDependencyCommandInjectableCommand();
            registerCmd.Execute();

            var result = Ioc.Resolve("Commands.CommandInjectable");
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ICommandInjectable>(result);
            Assert.IsType<CommandInjectableCommand>(result);
        }

        [Fact]
        public void Execute_ShouldRegisterDependency_ResolvableAsCommandInjectableCommand()
        {
            var registerCmd = new RegisterDependencyCommandInjectableCommand();
            registerCmd.Execute();

            var result = Ioc.Resolve("Commands.CommandInjectable");
            Assert.NotNull(result);
            Assert.IsType<CommandInjectableCommand>(result);
        }
    }
}