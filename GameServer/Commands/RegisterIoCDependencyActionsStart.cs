using System.Collections.Generic;
using GameServer.Interfaces;
using GameServer.Commands;

namespace GameServer.IoC
{
    public class RegisterIoCDependencyActionsStart : ICommand
    {
        public void Execute()
        {
            Ioc.Register("Actions.Start", (Func<object[], object>)(args =>
            {
                var order = (IDictionary<string, object>)args[0];
                return new StartActionCommand(order);
            }));
        }
    }
}
