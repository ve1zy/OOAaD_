#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class CommandInjectableCommand : ICommand
{
    private readonly ICommandInjectable _injectable;
    private readonly ICommand _command;

    public CommandInjectableCommand(ICommandInjectable injectable, ICommand command)
    {
        _injectable = injectable;
        _command = command;
    }

    public void Execute()
    {
        _injectable.Inject(_command);
    }
}
