#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class StartCommand : ICommand
{
    private readonly ICommand _command;

    public StartCommand(ICommand command)
    {
        _command = command;
    }

    public void Execute()
    {
        _command.Execute();
    }
}
