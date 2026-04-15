namespace GameServer.Interfaces;

public interface ICommandInjectable
{
    void InjectCommand(ICommand command);
}
