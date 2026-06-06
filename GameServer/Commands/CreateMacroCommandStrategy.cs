#nullable disable
using GameServer.Interfaces;
using GameServer.IoC;

namespace GameServer.Strategies;

public class CreateMacroCommandStrategy
{
    private readonly string _commandSpec;

    public CreateMacroCommandStrategy(string commandSpec)
    {
        _commandSpec = commandSpec;
    }

    public ICommand Resolve(params object[] args)
    {
        var spec = (string[])Ioc.Resolve("Specs." + _commandSpec);
        var commands = ResolveCommands(spec, 0);
        return new Commands.MacroCommand(commands);
    }

    private ICommand[] ResolveCommands(string[] specs, int index)
    {
        if (index >= specs.Length)
        {
            return Array.Empty<ICommand>();
        }

        var command = (ICommand)Ioc.Resolve(specs[index]);
        var rest = ResolveCommands(specs, index + 1);

        var result = new ICommand[rest.Length + 1];
        result[0] = command;
        Array.Copy(rest, 0, result, 1, rest.Length);
        return result;
    }
}
