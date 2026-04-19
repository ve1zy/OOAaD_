using GameServer.Interfaces;
using GameServer.IoC;

namespace GameServer.Commands;

/// <summary>
/// Command that registers the Commands.CommandInjectable dependency in IoC.
/// </summary>
public class RegisterDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.CommandInjectable", (object[] _) => new CommandInjectableCommand());
    }
}
