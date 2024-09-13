using System;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Intefraces;

namespace Skinet.Infrastructure.Data;

public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
{

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }
    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public bool Exists(int id)
    {
        return context.Set<T>().Any(e => e.Id == id);
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        var query = context.Set<T>().AsQueryable();
        query = specification.ApplyCriteria(query);
        return await query.CountAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), specification);
    }

    public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> specification)
    {
        return await ApplySpecification<TResult>(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
    {
        return SpecificationEvaluator<T>.GetQuery<TResult>(context.Set<T>().AsQueryable(), specification);
    }
}
