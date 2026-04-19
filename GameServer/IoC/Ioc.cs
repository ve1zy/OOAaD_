namespace GameServer.IoC;

public static class Ioc
{
    private static readonly Dictionary<string, object> _dependencies = new();
    private static readonly Dictionary<string, Func<object[], object>> _strategies = new();

    public static void Register(string key, object dependency)
    {
        _dependencies[key] = dependency;
    }

    public static void Register(string key, Func<object[], object> strategy)
    {
        _strategies[key] = strategy;
    }

    public static object Resolve(string key)
    {
        if (_dependencies.TryGetValue(key, out var dependency))
        {
            return dependency;
        }

        if (_strategies.TryGetValue(key, out var strategy))
        {
            return strategy(Array.Empty<object>());
        }

        throw new InvalidOperationException($"Dependency '{key}' not registered");
    }

    public static object Resolve(string key, params object[] args)
    {
        if (_strategies.TryGetValue(key, out var strategy))
        {
            return strategy(args);
        }

        throw new InvalidOperationException($"Strategy '{key}' not registered");
    }
}
