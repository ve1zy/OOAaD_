#nullable disable

namespace GameServer.IoC;

public class Ioc
{
    private static readonly Dictionary<string, object> _dependencies = new();
    private static readonly Dictionary<string, Func<object[], object>> _strategies = new();

    public static void Register(string key, Func<object[], object> strategy)
    {
        _strategies[key] = strategy;
    }

    public static void Register(string key, object dependency)
    {
        _dependencies[key] = dependency;
    }

    public static object Resolve(string key)
    {
        if (_strategies.TryGetValue(key, out var strategy))
        {
            return strategy(Array.Empty<object>());
        }

        if (_dependencies.TryGetValue(key, out var dependency))
        {
            return dependency;
        }

        throw new ArgumentException($"Dependency '{key}' not registered");
    }

    public static object Resolve(string key, object[] args)
    {
        if (_strategies.TryGetValue(key, out var strategy))
        {
            return strategy(args);
        }

        if (_dependencies.TryGetValue(key, out var dependency))
        {
            return dependency;
        }

        throw new ArgumentException($"Dependency '{key}' not registered");
    }
}
