// RegisterIoCDependencyActionsStart.cs
// ЗАДАНИЕ 19: Регистрация зависимости "Actions.Start"
// Адаптирован под реальные MoveCommand и RotateCommand из вашей команды
using System.Collections.Generic;
using GameServer.Interfaces;
using GameServer.Commands;
using GameServer.Models;

namespace GameServer.IoC
{
    public class RegisterIoCDependencyActionsStart : ICommand
    {
        public void Execute()
        {
            // Основная зависимость Actions.Start
            Ioc.Register("Actions.Start", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];

                var action = (string)order["action"];
                var queue = (Queue<ICommand>)order["queue"];

                // Получаем команду длительной операции
                var longOperationCommand = (ICommand)Ioc.Resolve($"Operations.{action}.Start", order);

                // Создаём инжектируемую команду
                var injectableCommand = (CommandInjectableCommand)Ioc.Resolve("Commands.CommandInjectable");
                injectableCommand.Inject(longOperationCommand);

                // Кладём в очередь
                queue.Enqueue(injectableCommand);

                return new EmptyCommand();
            }));

            // Регистрация операции движения
            // Ожидает в order: ["movableObject"] = IMovingObject, ["position"] = Vector
            Ioc.Register("Operations.move.Start", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];
                var movableObject = (IMovingObject)order["movableObject"];
                var position = (Vector)order["position"];
                return new MoveCommand(movableObject, position);
            }));

            // Регистрация операции вращения
            // Ожидает в order: ["rotatingObject"] = IRotatingObject
            Ioc.Register("Operations.rotate.Start", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];
                var rotatingObject = (IRotatingObject)order["rotatingObject"];
                return new RotateCommand(rotatingObject);
            }));
        }
    }
}
