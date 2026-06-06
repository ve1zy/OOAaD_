#nullable disable
using GameServer.Interfaces;

namespace GameServer.Commands;

public class SendCommand : ICommand
{
    private readonly ICommand _command;
    private readonly ICommandReceiver _receiver;

    public SendCommand(ICommand command, ICommandReceiver receiver)
    {
        _command = command;
        _receiver = receiver;
    }

    public void Execute()
    {
        if (_receiver == null)
        {
            throw new ArgumentException("Receiver cannot be null");
        }

        _receiver.Receive(_command);
    }
}
