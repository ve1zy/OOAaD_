#nullable disable

using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.IoC;

public class IocTests
{
    [Fact]
    public void RegisterAndResolve_SimpleDependency_ReturnsRegisteredObject()
    {
        var testObject = new object();
        Ioc.Register("TestKey", testObject);
        
        var resolved = Ioc.Resolve("TestKey");
        
        Assert.Same(testObject, resolved);
    }

    [Fact]
    public void RegisterAndResolve_StrategyWithNoArgs_ExecutesStrategy()
    {
        var executed = false;
        Ioc.Register("StrategyKey", (args) =>
        {
            executed = true;
            return new object();
        });
        
        Ioc.Resolve("StrategyKey");
        
        Assert.True(executed);
    }

    [Fact]
    public void RegisterAndResolve_StrategyWithArgs_PassesArgsToStrategy()
    {
        object[] receivedArgs = null;
        Ioc.Register("StrategyKey", (args) =>
        {
            receivedArgs = args;
            return new object();
        });
        
        var testArgs = new object[] { "arg1", "arg2" };
        Ioc.Resolve("StrategyKey", testArgs);
        
        Assert.Same(testArgs, receivedArgs);
    }

    [Fact]
    public void Resolve_UnregisteredKey_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Ioc.Resolve("NonExistentKey"));
    }

    [Fact]
    public void Register_OverwritesExistingDependency()
    {
        Ioc.Register("Key", "First");
        Ioc.Register("Key", "Second");
        
        var resolved = Ioc.Resolve("Key");
        
        Assert.Equal("Second", resolved);
    }
}
