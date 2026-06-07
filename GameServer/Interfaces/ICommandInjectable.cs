namespace GameServer.Interfaces
{
    public interface ICommandInjectable
    {
        void Inject(ICommand command);
    }
}
