using GameServer.IoC;

namespace GameServer.Commands;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        Ioc.Register("Actions.Start", args =>
        {
            var startAction = args[0] as Action ?? throw new ArgumentException("First argument must be an Action.");
            return new StartCommand(startAction);
        });
    }
}
