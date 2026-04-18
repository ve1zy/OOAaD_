using System;
using System.Collections.Generic;

namespace GameServer.IoC;

public static class Ioc
{
    private static readonly Dictionary<string, Func<object[], object>> _registrations = new();

    public static void Register(string key, Func<object[], object> factory)
    {
        _registrations[key] = factory;
    }

    public static T Resolve<T>(string key, params object[] args)
    {
        if (!_registrations.ContainsKey(key))
        {
            throw new InvalidOperationException($"No registration found for key: {key}");
        }

        var result = _registrations[key](args);
        if (result is T typedResult)
        {
            return typedResult;
        }

        throw new InvalidOperationException($"Registration for {key} does not return type {typeof(T).Name}");
    }

    public static void Clear()
    {
        _registrations.Clear();
    }
}
