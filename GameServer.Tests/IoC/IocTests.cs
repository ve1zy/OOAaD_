#nullable disable

using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.IoC;

public class IocTests
{
    [Fact]
    public void RegisterSingleton_WithInstance_ReturnsSameInstance()
    {
        var testObject = new object();
        Ioc.Instance.RegisterSingleton<object>(testObject);
        
        var resolved = Ioc.Instance.Resolve<object>();
        
        Assert.Same(testObject, resolved);
    }

    [Fact]
    public void RegisterSingleton_WithFactory_ReturnsSameInstance()
    {
        Ioc.Instance.RegisterSingleton<ITestService, TestService>();
        
        var resolved1 = Ioc.Instance.Resolve<ITestService>();
        var resolved2 = Ioc.Instance.Resolve<ITestService>();
        
        Assert.Same(resolved1, resolved2);
    }

    [Fact]
    public void RegisterTransient_ReturnsNewInstance()
    {
        Ioc.Instance.RegisterTransient<ITestService, TestService>();
        
        var resolved1 = Ioc.Instance.Resolve<ITestService>();
        var resolved2 = Ioc.Instance.Resolve<ITestService>();
        
        Assert.NotSame(resolved1, resolved2);
    }

    [Fact]
    public void Resolve_UnregisteredService_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Ioc.Instance.Resolve<ITestService>());
    }

    [Fact]
    public void ConstructorInjection_ResolvesDependencies()
    {
        Ioc.Instance.RegisterSingleton<IDependency, Dependency>();
        Ioc.Instance.RegisterTransient<ITestService, TestService>();
        
        var service = (TestService)Ioc.Instance.Resolve<ITestService>();
        
        Assert.NotNull(service);
        Assert.NotNull(service.Dependency);
    }

    [Fact]
    public void ConstructorInjection_WithMultipleDependencies_ResolvesAll()
    {
        Ioc.Instance.RegisterSingleton<IDependency, Dependency>();
        Ioc.Instance.RegisterSingleton<IAnotherDependency, AnotherDependency>();
        Ioc.Instance.RegisterTransient<IMultiDepService, MultiDepService>();
        
        var service = (MultiDepService)Ioc.Instance.Resolve<IMultiDepService>();
        
        Assert.NotNull(service);
        Assert.NotNull(service.Dependency);
        Assert.NotNull(service.AnotherDependency);
    }

    private interface ITestService { }
    private class TestService : ITestService { }

    private interface IDependency { }
    private class Dependency : IDependency { }

    private interface IAnotherDependency { }
    private class AnotherDependency : IAnotherDependency { }

    private interface IMultiDepService { }
    private class MultiDepService : IMultiDepService
    {
        public IDependency Dependency { get; }
        public IAnotherDependency AnotherDependency { get; }
        
        public MultiDepService(IDependency dependency, IAnotherDependency anotherDependency)
        {
            Dependency = dependency;
            AnotherDependency = anotherDependency;
        }
    }
}
