#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Commands.Send", (args) =>
        {
            var receiver = IoC.Ioc.Resolve(args[0].ToString());
            var command = IoC.Ioc.Resolve(args[1].ToString());
            return new SendCommand((ICommandReceiver)receiver, (ICommand)command);
        });
    }
}
