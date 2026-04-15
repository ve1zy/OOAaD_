namespace GameServer.Commands;

public class StopCommand : ICommand
{
    private readonly Action _stopAction;

    public StopCommand(Action stopAction)
    {
        _stopAction = stopAction ?? throw new ArgumentNullException(nameof(stopAction));
    }

    public void Execute()
    {
        _stopAction();
    }
}
