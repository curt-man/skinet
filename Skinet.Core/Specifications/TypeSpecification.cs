using System;
using System.Linq.Expressions;
using Skinet.Core.Entities;

namespace Skinet.Core.Specifications;

public class TypeSpecification(Expression<Func<Product, bool>>? criteria) : BaseSpecification<Product, string>(criteria)
{
    public TypeSpecification() : this(null)
    {
        AddSelect(p=>p.Type);
        ApplyDistinct();
    }
}
