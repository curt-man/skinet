using System;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;

namespace Skinet.Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.IsDistinct)
        {
            query = query.Distinct();
        }

        // Keyset (seek) pagination
        if (specification.SeekPredicate != null && specification.SeekValue != null)
        {
            query = query.Where(specification.SeekPredicate);
            query = query.Take(specification.Take);
        }
        // Offset-based pagination
        else if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }

    public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> query, ISpecification<T, TResult> specification)
    {
        if(specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if(specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if(specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        var selectQuery = query as IQueryable<TResult>;
        if(specification.Select != null)
        {
            selectQuery = query.Select(specification.Select);
        }
        
        if(specification.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }
        
        if(specification.IsPagingEnabled)
        {
            selectQuery = selectQuery?.Skip(specification.Skip).Take(specification.Take);
        }

        return selectQuery ?? query.Cast<TResult>();
    }
}
