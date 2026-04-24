#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class MacroCommand : ICommand
{
    private readonly ICommand[] _commands;

    public MacroCommand(ICommand[] commands)
    {
        _commands = commands ?? throw new ArgumentException("Commands cannot be null");
    }

    public void Execute()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }
}
