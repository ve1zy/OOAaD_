using GameServer.Interfaces;
using GameServer.IoC;

namespace GameServer.Commands;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Send", args =>
        {
            var receiver = Ioc.Resolve<ICommandReceiver>("Adapters.ICommandReceiver", args[0]);
            var command = Ioc.Resolve<ICommand>(args[1].ToString());
            return new SendCommand(receiver, command);
        });
    }
}
