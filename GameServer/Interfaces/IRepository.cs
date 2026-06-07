namespace GameServer.Interfaces;

public interface IRepository
{
    object GetById(string id);
    void Add(string id, object obj);
    void Delete(string id);
}