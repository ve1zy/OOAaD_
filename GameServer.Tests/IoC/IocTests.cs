using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.IoC;

public class IocTests
{
    [Fact]
    public void RegisterAndResolve_ReturnsRegisteredDependency()
    {
        var dependency = new object();
        Ioc.Register("TestKey", dependency);
        
        var resolved = Ioc.Resolve("TestKey");
        
        Assert.Same(dependency, resolved);
    }

    [Fact]
    public void Resolve_NotRegistered_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => Ioc.Resolve("NonExistentKey"));
    }

    [Fact]
    public void RegisterStrategy_WithArgs_ExecutesStrategy()
    {
        Ioc.Register("TestStrategy", (args) => args.Length);
        
        var result = Ioc.Resolve("TestStrategy", new object[] { 1, 2, 3 });
        
        Assert.Equal(3, result);
    }

    [Fact]
    public void RegisterStrategy_WithoutArgs_ExecutesStrategy()
    {
        Ioc.Register("TestStrategy", (args) => "StrategyResult");
        
        var result = Ioc.Resolve("TestStrategy");
        
        Assert.Equal("StrategyResult", result);
    }
}
