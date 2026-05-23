// CommandInjectableCommand.cs
using System;
using GameServer.Interfaces;

namespace GameServer.Commands
{
    public class CommandInjectableCommand : ICommand, ICommandInjectable
    {
        private ICommand? _injectedCommand;

        public void Inject(ICommand command)
        {
            _injectedCommand = command ?? throw new ArgumentNullException(nameof(command));
        }

        public void Execute()
        {
            if (_injectedCommand == null)
            {
                throw new InvalidOperationException("Command was not injected. Call Inject() before Execute().");
            }
            _injectedCommand.Execute();
        }
    }
}
