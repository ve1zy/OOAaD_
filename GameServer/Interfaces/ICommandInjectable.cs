using GameServer.Interfaces;

namespace GameServer.Interfaces;

/// <summary>
/// Interface for commands that can have another command injected into them.
/// </summary>
public interface ICommandInjectable
{
    /// <summary>
    /// Injects a command into this object.
    /// </summary>
    /// <param name="command">The command to inject.</param>
    void Inject(ICommand command);
}
