using DotNetPIS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetPIS.Data.Repositories;

public class Repository<T,K> : IRepository<T,K> where T : class
{
    private readonly DbSet<T> _dbSet;

    public Repository(Context context)
    {
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet;
    }

    public T GetById(K id)
    {
        T entity = _dbSet.Find(id) ?? throw new KeyNotFoundException($"Entity with ID {id} not found.");

        return entity;
    }

    public void Add(T obj)
    {
        _dbSet.Add(obj);
    }

    public void Remove(T obj)
    {
        _dbSet.Remove(obj);
    }
}