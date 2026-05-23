#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterActionsStopCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Actions.Stop", (args) =>
        {
            var command = IoC.Ioc.Resolve(args[0].ToString());
            return new StopCommand((ICommand)command);
        });
    }
}
