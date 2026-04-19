using GameServer.IoC;

namespace GameServer.Commands;

public class RegisterIoCDependencyCommandsStop : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Stop", args =>
        {
            var stopAction = args[0] as Action ?? throw new ArgumentException("First argument must be an Action.");
            return new StopCommand(stopAction);
        });
    }
}
