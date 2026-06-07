// RegisterDependencyCommandInjectableCommand.cs
using GameServer.Interfaces;
using GameServer.IoC;

namespace GameServer.Commands
{
    public class RegisterDependencyCommandInjectableCommand : ICommand
    {
        public void Execute()
        {
            Ioc.Register("Commands.CommandInjectable", args => new CommandInjectableCommand());
        }
    }
}