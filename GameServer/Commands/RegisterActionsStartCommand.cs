#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterActionsStartCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Actions.Start", (args) =>
        {
            var command = IoC.Ioc.Resolve(args[0].ToString());
            return new StartCommand((ICommand)command);
        });
    }
}
