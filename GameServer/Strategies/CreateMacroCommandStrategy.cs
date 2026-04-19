using GameServer.Commands;
using GameServer.IoC;

namespace GameServer.Strategies;

public class CreateMacroCommandStrategy
{
    public static ICommand Resolve(string macroKey)
    {
        var specKey = "Specs." + macroKey;
        var commandNames = Ioc.Resolve<string[]>(specKey);

        if (commandNames == null || commandNames.Length == 0)
        {
            throw new InvalidOperationException($"No commands found in spec for {macroKey}");
        }

        var commands = new ICommand[commandNames.Length];
        for (int i = 0; i < commandNames.Length; i++)
        {
            commands[i] = Ioc.Resolve<ICommand>(commandNames[i]);
        }

        return new MacroCommand(commands);
    }
}
