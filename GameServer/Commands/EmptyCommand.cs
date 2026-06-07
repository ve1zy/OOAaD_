using GameServer.Interfaces;

namespace GameServer.Commands
{
    public class EmptyCommand : ICommand
    {
        public void Execute() { }
    }
}