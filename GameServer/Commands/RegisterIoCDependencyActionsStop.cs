using GameServer.IoC;

namespace GameServer.Commands;

public class RegisterIoCDependencyActionsStop : ICommand
{
    public void Execute()
    {
        Ioc.Register("Actions.Stop", args =>
        {
            var stopAction = args[0] as Action ?? throw new ArgumentException("First argument must be an Action.");
            return new StopCommand(stopAction);
        });
    }
}
