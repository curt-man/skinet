using System;
using System.Linq.Expressions;
using Skinet.Core.Entities;

namespace Skinet.Core.Specifications;

public class ProductSpecification(Expression<Func<Product, bool>>? criteria) : BaseSpecification<Product>(criteria)
{
    public ProductSpecification(ProductSpecificationParameters parameters) : this(p =>
        (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.ToLower().Contains(parameters.Search)) &&
        (!parameters.Brands.Any() || parameters.Brands.Contains(p.Brand)) &&
        (!parameters.Types.Any() || parameters.Types.Contains(p.Type)))
    {
        ApplyPaging(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);

        switch (parameters.Sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }
} 
