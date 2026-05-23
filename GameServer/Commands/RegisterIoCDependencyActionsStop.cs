// RegisterIoCDependencyActionsStop.cs
using GameServer.Interfaces;
using GameServer.Commands;

namespace GameServer.IoC
{
    public class RegisterIoCDependencyActionsStop : ICommand
    {
        public void Execute()
        {
            // Регистрируем пустую команду
            Ioc.Register("Commands.Empty", (Func<object[], object>)(_ => new EmptyCommand()));

            // Основная зависимость Actions.Stop
            Ioc.Register("Actions.Stop", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];
                var injectableCommand = (ICommandInjectable)order["command"];
                var emptyCommand = (ICommand)Ioc.Resolve("Commands.Empty");
                injectableCommand.Inject(emptyCommand);
                return new EmptyCommand();
            }));
        }
    }
}
