using GameServer.Interfaces;

namespace GameServer.Commands;

public class CommandInjectableCommand : ICommand, ICommandInjectable
{
    private ICommand _injectedCommand;

    public CommandInjectableCommand()
    {
        _injectedCommand = null;
    }

    public void InjectCommand(ICommand command)
    {
        _injectedCommand = command ?? throw new ArgumentNullException(nameof(command));
    }

    public void Execute()
    {
        if (_injectedCommand == null)
        {
            throw new InvalidOperationException("No command has been injected.");
        }

        _injectedCommand.Execute();
    }
}
