namespace DotNetPIS.Domain.Interfaces;

public interface IRepository<T,K>
{
    IQueryable<T> GetAll();
    T GetById(K id);
    void Add(T obj);
    void Remove(T obj);
}