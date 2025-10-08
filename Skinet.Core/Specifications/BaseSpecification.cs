using System;
using System.Linq.Expressions;
using Skinet.Core.Interfaces;

namespace Skinet.Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria = null) : ISpecification<T>
{
    protected BaseSpecification() : this(null) {}
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct {get; private set;}

    public int Take {get; private set;}
    public int Skip {get; private set;}
    public bool IsPagingEnabled {get; private set;}
    public object? SeekValue { get; private set; }
    public Expression<Func<T, bool>>? SeekPredicate { get; private set; }

    public List<Expression<Func<T, object>>> Includes {get; } = [];
    public List<string> IncludeStrings {get; } = [];

    int ISpecification<T>.Take => Take;

    int ISpecification<T>.Skip => Skip;

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if(Criteria != null)
        {
            query = query.Where(Criteria);
        }
        return query;   
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpressions)
    {
        Includes.Add(includeExpressions);
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpressionDescending)
    {
        OrderByDescending = orderByExpressionDescending;
    }
    
    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

    protected void ApplyPaging(int take, int skip = default, object? seekValue = null, Expression<Func<T, bool>>? seekPredicate = null)
    {
        Take = take;
        Skip = skip;
        SeekValue = seekValue;
        SeekPredicate = seekPredicate;
        IsPagingEnabled = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria = null) : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null) {}

    public Expression<Func<T, TResult>>? Select {get; private set;}

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
    
}
