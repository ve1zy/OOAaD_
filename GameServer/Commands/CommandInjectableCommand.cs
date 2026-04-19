using GameServer.Interfaces;

namespace GameServer.Commands;

/// <summary>
/// Command that implements both ICommand and ICommandInjectable interfaces.
/// Allows injection of a command that will be executed when this command's Execute is called.
/// </summary>
public class CommandInjectableCommand : ICommand, ICommandInjectable
{
    private ICommand? _injectedCommand;

    /// <summary>
    /// Injects a command into this object.
    /// </summary>
    /// <param name="command">The command to inject.</param>
    public void Inject(ICommand command)
    {
        _injectedCommand = command ?? throw new ArgumentNullException(nameof(command));
    }

    /// <summary>
    /// Executes the injected command.
    /// </summary>
    public void Execute()
    {
        if (_injectedCommand == null)
        {
            throw new InvalidOperationException("No command has been injected. Call Inject() before Execute().");
        }

        _injectedCommand.Execute();
    }
}
