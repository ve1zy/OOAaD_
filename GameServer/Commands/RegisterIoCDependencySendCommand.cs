#nullable disable
using GameServer.Interfaces;
using GameServer.IoC;

namespace GameServer.Commands;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        Ioc.Register("Commands.Send", (args) =>
        {
            var command = (ICommand)args[0];
            var receiver = (ICommandReceiver)args[1];
            return new SendCommand(command, receiver);
        });
    }
}
