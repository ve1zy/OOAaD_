using Xunit;
using System.Collections.Generic;
using Moq;
using GameServer.Interfaces;
using GameServer.Commands;
using GameServer.IoC;

namespace GameServer.Tests
{
    [Collection("IoC Tests")]
    public class StopActionCommandTests
    {
        public StopActionCommandTests()
        {
            Ioc.Clear();
        }

        [Fact]
        public void Execute_ShouldInjectEmptyCommand_IntoInjectable()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();

            var longOpMock = new Mock<ICommand>();
            var injectable = (CommandInjectableCommand)Ioc.Resolve("Commands.CommandInjectable");
            injectable.Inject(longOpMock.Object);

            var order = new Dictionary<string, object>
            {
                ["injectable"] = injectable
            };

            var cmd = new StopActionCommand(order);
            cmd.Execute();

            // После остановки injectable вызывает EmptyCommand, а не longOp
            injectable.Execute();
            longOpMock.Verify(m => m.Execute(), Times.Never);
        }

        [Fact]
        public void Execute_WithoutInjectableKey_ShouldThrowKeyNotFoundException()
        {
            var order = new Dictionary<string, object>();

            var cmd = new StopActionCommand(order);
            Assert.Throws<KeyNotFoundException>(() => cmd.Execute());
        }


        [Fact]
        public void Execute_StopIsO1_DoesNotIterateQueue()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();

            var injectable = (CommandInjectableCommand)Ioc.Resolve("Commands.CommandInjectable");
            injectable.Inject(new Mock<ICommand>().Object);

            var bigQueue = new Queue<ICommand>();
            for (int i = 0; i < 1000; i++)
                bigQueue.Enqueue(new Mock<ICommand>().Object);

            var order = new Dictionary<string, object>
            {
                ["injectable"] = injectable,
                ["queue"] = bigQueue
            };

            var cmd = new StopActionCommand(order);
            cmd.Execute();

            // Очередь не изменилась - StopActionCommand её не трогал
            Assert.Equal(1000, bigQueue.Count);
        }

        [Fact]
        public void FullCycle_StartThenStop_ShouldReplaceLongOpWithEmpty()
        {
            new RegisterDependencyCommandInjectableCommand().Execute();
            new RegisterIoCDependencyActionsStart().Execute();
            new RegisterIoCDependencyActionsStop().Execute();

            var moveMock = new Mock<ICommand>();
            Ioc.Register("Operations.move.Start", (Func<object[], object>)(args => moveMock.Object));

            var queue = new Queue<ICommand>();

            // --- Шаг 1: Start ---
            var startOrder = new Dictionary<string, object>
            {
                ["action"] = "move",
                ["queue"] = queue
            };

            var startCmd = (ICommand)Ioc.Resolve("Actions.Start", startOrder);
            startCmd.Execute();

            Assert.Single(queue);
            var injectable = (CommandInjectableCommand)queue.Peek();

            // До остановки - injectable выполняет longOp
            injectable.Execute();
            moveMock.Verify(m => m.Execute(), Times.Once);

            // --- Шаг 2: Stop ---
            var stopOrder = new Dictionary<string, object>
            {
                ["injectable"] = startOrder["injectable"]
            };

            var stopCmd = (ICommand)Ioc.Resolve("Actions.Stop", stopOrder);
            stopCmd.Execute();

            
            injectable.Execute();
            moveMock.Verify(m => m.Execute(), Times.Once); 
        }
    }
}