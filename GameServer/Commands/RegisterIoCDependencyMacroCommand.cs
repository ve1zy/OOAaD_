#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Commands.Macro", (args) =>
        {
            var commands = new ICommand[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                commands[i] = (ICommand)IoC.Ioc.Resolve(args[i].ToString());
            }
            return new MacroCommand(commands);
        });
    }
}
