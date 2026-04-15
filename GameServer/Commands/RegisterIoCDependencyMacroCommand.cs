using GameServer.IoC;

namespace GameServer.Commands;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Macro", args =>
        {
            ICommand[] commands;

            if (args.Length > 0 && args[0] is ICommand[] cmdArray)
            {
                commands = cmdArray;
            }
            else if (args.Length > 0 && args[0] is object[] objArray && objArray.All(o => o is ICommand))
            {
                commands = objArray.Cast<ICommand>().ToArray();
            }
            else
            {
                throw new ArgumentException("Macro command requires an array of ICommand objects.");
            }

            return new MacroCommand(commands);
        });
    }
}
