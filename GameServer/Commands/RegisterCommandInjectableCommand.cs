#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Commands.CommandInjectable", (args) =>
        {
            var injectable = IoC.Ioc.Resolve(args[0].ToString());
            var command = IoC.Ioc.Resolve(args[1].ToString());
            return new CommandInjectableCommand((ICommandInjectable)injectable, (ICommand)command);
        });
    }
}
