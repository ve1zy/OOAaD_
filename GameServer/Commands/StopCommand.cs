#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class StopCommand : ICommand
{
    private readonly ICommand _command;

    public StopCommand(ICommand command)
    {
        _command = command;
    }

    public void Execute()
    {
        _command.Execute();
    }
}
