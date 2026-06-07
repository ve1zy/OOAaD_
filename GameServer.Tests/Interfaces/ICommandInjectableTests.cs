using Xunit;
using Moq;
using GameServer.Interfaces;

namespace GameServer.Tests
{
    public class FakeCommandInjectable : ICommandInjectable
    {
        public ICommand? InjectedCommand { get; private set; }
        public bool WasCalled { get; private set; }

        public void Inject(ICommand command)
        {
            InjectedCommand = command;
            WasCalled = true;
        }
    }

    public class ICommandInjectableTests
    {
        [Fact]
        public void Interface_HasInjectMethod()
        {
            // Arrange — проверяем, что интерфейс определён корректно
            var fake = new FakeCommandInjectable();
            var mockCommand = new Mock<ICommand>();

            // Act — вызываем метод интерфейса
            fake.Inject(mockCommand.Object);

            // Assert — метод работает и принимает ICommand
            Assert.True(fake.WasCalled);
            Assert.Same(mockCommand.Object, fake.InjectedCommand);
        }

        [Fact]
        public void Interface_AcceptsDifferentCommands()
        {
            // Arrange
            var fake = new FakeCommandInjectable();
            var mockCommand1 = new Mock<ICommand>();
            var mockCommand2 = new Mock<ICommand>();

            // Act
            fake.Inject(mockCommand1.Object);
            var first = fake.InjectedCommand;

            fake.Inject(mockCommand2.Object);
            var second = fake.InjectedCommand;

            // Assert — можно инжектить разные команды
            Assert.Same(mockCommand1.Object, first);
            Assert.Same(mockCommand2.Object, second);
        }

        [Fact]
        public void Interface_MethodSignature_IsCorrect()
        {
            // Arrange — получаем метод интерфейса через рефлексию
            var interfaceType = typeof(ICommandInjectable);
            var injectMethod = interfaceType.GetMethod("Inject");

            // Assert — проверяем сигнатуру метода
            Assert.NotNull(injectMethod);
            Assert.Equal(typeof(void), injectMethod.ReturnType);

            var parameters = injectMethod.GetParameters();
            Assert.Single(parameters);
            Assert.Equal("command", parameters[0].Name);
            Assert.Equal(typeof(ICommand), parameters[0].ParameterType);
        }

        [Fact]
        public void FakeImplementation_CanBeUsedAsICommandInjectable()
        {
            // Arrange — проверяем, что fake реализация реализует интерфейс
            var fake = new FakeCommandInjectable();

            // Act & Assert — можно привести к интерфейсу
            Assert.IsAssignableFrom<ICommandInjectable>(fake);
        }
    }
}