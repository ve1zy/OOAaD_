using GameServer.IoC;

namespace GameServer.Commands;

public class RegisterDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.CommandInjectable", _ => new CommandInjectableCommand());
    }
}
