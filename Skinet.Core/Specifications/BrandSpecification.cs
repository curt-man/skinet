using System;
using System.Linq.Expressions;
using Skinet.Core.Entities;

namespace Skinet.Core.Specifications;

public class BrandSpecification : BaseSpecification<Product, string>
{
    public BrandSpecification() : base()
    {
        AddSelect(p => p.Brand);
        ApplyDistinct();
    }
}
