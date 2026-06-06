using GameServer.Commands;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.Commands;

public class RegisterIoCDependencyMacroMoveRotateTests
{
    [Fact]
    public void Execute_RegistersSpecsMoveInIoc()
    {
        Ioc.Clear();
        var command = new RegisterIoCDependencyMacroMoveRotate();
        command.Execute();

        var spec = Ioc.Resolve("Specs.Move");
        var moveSpec = Assert.IsType<string[]>(spec);
        Assert.Contains("Commands.Move", moveSpec);
    }

    [Fact]
    public void Execute_RegistersSpecsRotateInIoc()
    {
        Ioc.Clear();
        var command = new RegisterIoCDependencyMacroMoveRotate();
        command.Execute();

        var spec = Ioc.Resolve("Specs.Rotate");
        var rotateSpec = Assert.IsType<string[]>(spec);
        Assert.Contains("Commands.Rotate", rotateSpec);
    }
}