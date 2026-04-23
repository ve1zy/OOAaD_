#nullable disable

namespace GameServer.IoC;

public enum ServiceLifetime
{
    Transient,
    Singleton
}

public class ServiceDescriptor
{
    public Type ServiceType { get; }
    public Type ImplementationType { get; }
    public object? Instance { get; set; }
    public ServiceLifetime Lifetime { get; }

    public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
    {
        ServiceType = serviceType;
        ImplementationType = implementationType;
        Lifetime = lifetime;
    }

    public ServiceDescriptor(Type serviceType, object instance)
    {
        ServiceType = serviceType;
        Instance = instance;
        Lifetime = ServiceLifetime.Singleton;
    }
}

public class Ioc
{
    private readonly Dictionary<Type, ServiceDescriptor> _services = new();
    private static Ioc? _instance;

    public static Ioc Instance => _instance ??= new Ioc();

    private Ioc() { }

    public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
    {
        _services[typeof(TService)] = new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton);
    }

    public void RegisterSingleton<TService>(TService instance)
    {
        _services[typeof(TService)] = new ServiceDescriptor(typeof(TService), instance);
    }

    public void RegisterTransient<TService, TImplementation>() where TImplementation : TService
    {
        _services[typeof(TService)] = new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);
    }

    public TService Resolve<TService>()
    {
        return (TService)Resolve(typeof(TService));
    }

    public object Resolve(Type serviceType)
    {
        if (!_services.TryGetValue(serviceType, out var descriptor))
        {
            throw new ArgumentException($"Service '{serviceType.Name}' not registered");
        }

        if (descriptor.Instance != null)
        {
            return descriptor.Instance;
        }

        if (descriptor.Lifetime == ServiceLifetime.Singleton && descriptor.Instance != null)
        {
            return descriptor.Instance;
        }

        var instance = CreateInstance(descriptor.ImplementationType);

        if (descriptor.Lifetime == ServiceLifetime.Singleton)
        {
            descriptor.Instance = instance;
        }

        return instance;
    }

    private object CreateInstance(Type type)
    {
        var constructors = type.GetConstructors();
        if (constructors.Length == 0)
        {
            return Activator.CreateInstance(type);
        }

        var constructor = constructors[0];
        var parameters = constructor.GetParameters();
        var args = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            args[i] = Resolve(parameters[i].ParameterType);
        }

        return constructor.Invoke(args);
    }
}
