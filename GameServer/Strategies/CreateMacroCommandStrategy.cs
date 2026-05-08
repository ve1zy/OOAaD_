#nullable disable
using GameServer.Interfaces;

namespace GameServer.Strategies;

public class CreateMacroCommandStrategy
{
    public ICommand CreateMacroCommand(object[] commandKeys)
    {
        if (commandKeys == null || commandKeys.Length == 0)
        {
            throw new ArgumentException("Command keys cannot be null or empty");
        }

        var commands = new ICommand[commandKeys.Length];
        for (int i = 0; i < commandKeys.Length; i++)
        {
            if (commandKeys[i] == null)
            {
                throw new ArgumentException($"Command key at index {i} cannot be null or empty");
            }

            var commandKey = commandKeys[i].ToString();
            if (string.IsNullOrEmpty(commandKey))
            {
                throw new ArgumentException($"Command key at index {i} cannot be null or empty");
            }
            commands[i] = (ICommand)IoC.Ioc.Resolve(commandKey);
        }

        return new Commands.MacroCommand(commands);
    }
}
