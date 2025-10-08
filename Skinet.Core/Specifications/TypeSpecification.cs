using System;
using System.Linq.Expressions;
using Skinet.Core.Entities;

namespace Skinet.Core.Specifications;

public class TypeSpecification : BaseSpecification<Product, string>
{
    public TypeSpecification() : base(null)
    {
        AddSelect(p=>p.Type);
        ApplyDistinct();
    }
}
