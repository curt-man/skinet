using System;
using System.Linq.Expressions;

namespace Skinet.Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }

    bool IsDistinct { get; }

    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    object? SeekValue { get; }
    Expression<Func<T, bool>>? SeekPredicate { get; }

    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }

    IQueryable<T> ApplyCriteria(IQueryable<T> query);

}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? Select { get; }
}