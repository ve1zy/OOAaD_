#nullable disable

using GameServer.Commands;
using GameServer.Interfaces;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

[Collection("Sequential")]
public class RegisterIoCDependencyMoveCommandTests
{
    public RegisterIoCDependencyMoveCommandTests()
    {
        Ioc.Instance.Clear();
    }

    [Fact]
    public void Execute_RegistersDependencies()
    {
        var command = new RegisterIoCDependencyMoveCommand();
        
        command.Execute();
        
        var movableObject = Ioc.Instance.Resolve<IMovingObject>();
        var position = Ioc.Instance.Resolve<GameServer.Models.Vector>();
        var moveCommand = Ioc.Instance.Resolve<ICommand>();
        
        Assert.NotNull(movableObject);
        Assert.NotNull(position);
        Assert.NotNull(moveCommand);
        Assert.IsType<MoveCommand>(moveCommand);
    }

    [Fact]
    public void Execute_MovableObjectIsSingleton_ReturnsSameInstance()
    {
        var command = new RegisterIoCDependencyMoveCommand();
        command.Execute();
        
        var movableObject1 = Ioc.Instance.Resolve<IMovingObject>();
        var movableObject2 = Ioc.Instance.Resolve<IMovingObject>();
        
        Assert.Same(movableObject1, movableObject2);
    }

    [Fact]
    public void Execute_MoveCommandIsTransient_ReturnsNewInstance()
    {
        var command = new RegisterIoCDependencyMoveCommand();
        command.Execute();
        
        var moveCommand1 = Ioc.Instance.Resolve<ICommand>();
        var moveCommand2 = Ioc.Instance.Resolve<ICommand>();
        
        Assert.NotSame(moveCommand1, moveCommand2);
    }
}
