using System;
using GameServer.IoC;
using Xunit;

namespace GameServer.Tests.IoC;

public class IocTests
{
    public IocTests()
    {
        Ioc.Clear();
    }

    [Fact]
    public void RegisterAndResolve_WithValidRegistration_ReturnsValue()
    {
        Ioc.Register("test.key", _ => "test-value");
        var result = Ioc.Resolve<string>("test.key");
        Assert.Equal("test-value", result);
    }

    [Fact]
    public void Resolve_WithMissingKey_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => Ioc.Resolve<string>("missing.key"));
    }

    [Fact]
    public void Resolve_WithWrongType_ThrowsInvalidOperationException()
    {
        Ioc.Register("test.key", _ => "string-value");
        Assert.Throws<InvalidOperationException>(() => Ioc.Resolve<int>("test.key"));
    }

    [Fact]
    public void Register_WithArgs_PassesArgsToFactory()
    {
        Ioc.Register("test.key", args => args.Length > 0 ? args[0] : "no-args");
        var result = Ioc.Resolve<string>("test.key", "arg-value");
        Assert.Equal("arg-value", result);
    }

    [Fact]
    public void Clear_RemovesAllRegistrations()
    {
        Ioc.Register("test.key", _ => "value");
        Ioc.Clear();
        Assert.Throws<InvalidOperationException>(() => Ioc.Resolve<string>("test.key"));
    }

    [Fact]
    public void Register_OverwritesPreviousRegistration()
    {
        Ioc.Register("test.key", _ => "first");
        Ioc.Register("test.key", _ => "second");
        var result = Ioc.Resolve<string>("test.key");
        Assert.Equal("second", result);
    }

    [Fact]
    public void Resolve_WithMultipleArgs_PassesAllArgs()
    {
        Ioc.Register("test.key", args => args.Length);
        var result = Ioc.Resolve<int>("test.key", "a", "b", "c");
        Assert.Equal(3, result);
    }
}
