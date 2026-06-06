using System;
using System.Collections.Generic;
using GameServer.Interfaces;
using GameServer.Commands;

namespace GameServer.IoC
{
    /// <summary>
    /// Регистрация зависимости "Actions.Stop".
    /// IoC-лямбда ТОЛЬКО создаёт объект (фабрика), вся логика — в StopActionCommand.
    /// </summary>
    public class RegisterIoCDependencyActionsStop : ICommand
    {
        public void Execute()
        {
            Ioc.Register("Actions.Stop", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];
                return new StopActionCommand(order);
            }));
        }
    }
}
