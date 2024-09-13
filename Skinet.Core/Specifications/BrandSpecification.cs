using System;
using System.Linq.Expressions;
using Skinet.Core.Entities;

namespace Skinet.Core.Specifications;

public class BrandSpecification(Expression<Func<Product, bool>>? criteria) : BaseSpecification<Product, string>(criteria)
{
    public BrandSpecification() : this(null)
    {
        AddSelect(p => p.Brand);
        ApplyDistinct();
    }
}
