namespace GameServer.Repositories;

public class GameObjectsRepository : IGameObjectsRepository
{
    private readonly Dictionary<string, object> _objects = new();

    public T? Get<T>(string id) where T : class
    {
        if (_objects.TryGetValue(id, out var obj))
        {
            return obj as T;
        }
        return null;
    }

    public void Add<T>(string id, T obj) where T : class
    {
        _objects[id] = obj;
    }

    public void Remove<T>(string id) where T : class
    {
        _objects.Remove(id);
    }

    public IEnumerable<T> GetAll<T>() where T : class
    {
        return _objects.Values.OfType<T>();
    }
}