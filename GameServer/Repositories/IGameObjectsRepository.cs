namespace GameServer.Repositories;

public interface IGameObjectsRepository
{
    T Get<T>(string id) where T : class;
    void Add<T>(string id, T obj) where T : class;
    void Remove<T>(string id) where T : class;
    IEnumerable<T> GetAll<T>() where T : class;
}