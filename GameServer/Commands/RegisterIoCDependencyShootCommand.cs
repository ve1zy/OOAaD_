using GameServer.Interfaces;
using GameServer.Models;
using GameServer.Repositories;
using GameServer.Services;

namespace GameServer.Commands;

public class RegisterIoCDependencyShootCommand : ICommand
{
    public void Execute()
    {
        IoC.Ioc.Register("Repositories.GameObjects", (args) => new GameObjectsRepository());
        IoC.Ioc.Register("Services.Authorization", (args) => new AuthorizationService());
        IoC.Ioc.Register("Commands.Shoot", (args) =>
        {
            var ship = args[0] as IShip;
            var repository = (IGameObjectsRepository)IoC.Ioc.Resolve("Repositories.GameObjects");
            return new ShootCommand(ship, repository);
        });
    }
}