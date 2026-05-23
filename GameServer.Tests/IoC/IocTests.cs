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
    [Fact]
    public void Resolve_WithStrategyAndArgs_InvokesStrategy()
    {
        Ioc.Clear();
        // Регистрируем стратегию, ожидающую аргументы
        Ioc.Register("StrategyKey", (args) => $"Processed: {args[0]}");

        var result = Ioc.Resolve("StrategyKey", "input_data");

        Assert.Equal("Processed: input_data", result);
    }

    [Fact]
    public void Resolve_WithDirectObject_IgnoresPassedArgs()
    {
        Ioc.Clear();
        var directObj = new object();
        Ioc.Register("DirectKey", directObj);

        // Передаём аргументы, но должен вернуться прямой объект без вызова стратегии
        var result = Ioc.Resolve("DirectKey", "ignored_arg");

        Assert.Same(directObj, result);
    }

    [Fact]
    public void Resolve_WithEmptyArgsArray_DoesNotFail()
    {
        Ioc.Clear();
        Ioc.Register("EmptyArgsKey", (args) => args?.Length ?? -1);

        var result = Ioc.Resolve("EmptyArgsKey", Array.Empty<object>());

        Assert.Equal(0, result);
    }
}
