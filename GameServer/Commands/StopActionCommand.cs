using System.Collections.Generic;
using GameServer.Interfaces;
using GameServer.Commands;

namespace GameServer.Commands
{
    /// <summary>
    /// Команда остановки длительной операции.
    /// Выполняется за O(1): просто инжектирует EmptyCommand в CommandInjectableCommand,
    /// заменяя длинную операцию на пустую.
    /// </summary>
    public class StopActionCommand : ICommand
    {
        private readonly IDictionary<string, object> _order;

        public StopActionCommand(IDictionary<string, object> order)
        {
            _order = order;
        }

        public void Execute()
        {
            var injectable = (CommandInjectableCommand)_order["injectable"];
            injectable.Inject(new EmptyCommand());
        }
    }
}
