#nullable disable
using GameServer.Interfaces;
using System.Linq;

namespace GameServer.Strategies;

public class CreateMacroCommandStrategy
{
    public ICommand CreateMacroCommand(object[] commandKeys)
    {
        if (commandKeys == null || commandKeys.Length == 0)
        {
            throw new ArgumentException("Command keys cannot be null or empty");
        }

        var commands = commandKeys.Select((key, index) =>
        {
            if (key == null)
                throw new ArgumentException($"Command key at index {index} cannot be null or empty");
            var commandKey = key.ToString();
            if (string.IsNullOrEmpty(commandKey))
                throw new ArgumentException($"Command key at index {index} cannot be null or empty");
            return (ICommand)IoC.Ioc.Resolve(commandKey);
        }).ToArray();

        return new Commands.MacroCommand(commands);
    }
}
