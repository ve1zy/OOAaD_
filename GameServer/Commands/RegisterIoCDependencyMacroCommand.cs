#nullable disable
using GameServer.Interfaces;
using GameServer.IoC;

namespace GameServer.Commands;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Macro", (args) =>
        {
            var commands = (ICommand[])args[0];
            return new MacroCommand(commands);
        });
    }
}
