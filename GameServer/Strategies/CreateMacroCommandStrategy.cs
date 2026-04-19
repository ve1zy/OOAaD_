using GameServer.Interfaces;

namespace GameServer.Strategies;

public class CreateMacroCommandStrategy
{
    public ICommand CreateMacroCommand(string[] commandNames)
    {
        var commands = new ICommand[commandNames.Length];
        for (int i = 0; i < commandNames.Length; i++)
        {
            commands[i] = (ICommand)IoC.Ioc.Resolve(commandNames[i]);
        }
        return new Commands.MacroCommand(commands);
    }
}
