#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class SendCommand : ICommand
{
    private readonly ICommandReceiver _receiver;
    private readonly ICommand _command;

    public SendCommand(ICommandReceiver receiver, ICommand command)
    {
        _receiver = receiver;
        _command = command;
    }

    public void Execute()
    {
        _receiver.Receive(_command);
    }
}
