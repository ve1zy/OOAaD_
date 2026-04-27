using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.IoC;

[Collection("IocTests")]
public class IocTests
{
    [Fact]
    public void Register_WithDependency_ReturnsSameInstance()
    {
        Ioc.Clear();
        var testObject = new object();
        Ioc.Register("TestKey", testObject);

        var resolved = Ioc.Resolve("TestKey");

        Assert.Same(testObject, resolved);
    }

    [Fact]
    public void Register_WithStrategy_ResolvesWithArgs()
    {
        Ioc.Clear();
        Ioc.Register("TestStrategy", (args) => args[0]);

        var resolved = Ioc.Resolve("TestStrategy", "result");

        Assert.Equal("result", resolved);
    }

    [Fact]
    public void Resolve_UnregisteredDependency_ThrowsInvalidOperationException()
    {
        Ioc.Clear();
        Assert.Throws<InvalidOperationException>(() => Ioc.Resolve("UnregisteredKey"));
    }
}
