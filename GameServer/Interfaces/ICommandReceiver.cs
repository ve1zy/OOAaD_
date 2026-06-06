using GameServer.Interfaces;

namespace GameServer.Interfaces;

public interface ICommandReceiver
{
    void Receive(ICommand command);
}
