using GameServer.Interfaces;
using GameServer.IoC;
using System.Collections.Generic;


namespace GameServer.Commands
{
    public class StartActionCommand : ICommand
    {
        private readonly IDictionary<string, object> _order;

        public StartActionCommand(IDictionary<string, object> order)
        {
            _order = order;
        }

        public void Execute()
        {
            var action = (string)_order["action"];
            var queue = (Queue<ICommand>)_order["queue"];

            var longOperationCommand = (ICommand)Ioc.Resolve($"Operations.{action}.Start", _order);

            var injectableCommand = (CommandInjectableCommand)Ioc.Resolve("Commands.CommandInjectable");
            injectableCommand.Inject(longOperationCommand);

            queue.Enqueue(injectableCommand);
        }
    }
}
