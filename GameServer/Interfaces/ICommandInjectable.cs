#nullable disable
using GameServer.Interfaces;

namespace GameServer.Interfaces;

public interface ICommandInjectable
{
    void Inject(ICommand command);
}
