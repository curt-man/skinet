using System;
using System.Collections.Concurrent;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;

namespace Skinet.Infrastructure.Data;

public class UnitOfWork(StoreContext context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(type, t =>
        {
            var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));
            return Activator.CreateInstance(repositoryType, context)
                ?? throw new InvalidOperationException($"Cound not create repository instance for {t}");
        });
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
