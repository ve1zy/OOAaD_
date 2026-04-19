namespace GameServer.Interfaces;

public interface ICommandInjectable
{
    ICommand ResolveCommand(string key);
}
