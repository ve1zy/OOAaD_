namespace GameServer.Commands;

public class StartCommand : ICommand
{
    private readonly Action _startAction;

    public StartCommand(Action startAction)
    {
        _startAction = startAction ?? throw new ArgumentNullException(nameof(startAction));
    }

    public void Execute()
    {
        _startAction();
    }
}
