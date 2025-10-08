using System;
using System.Linq.Expressions;
using Skinet.Core.Entities;

namespace Skinet.Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParameters parameters) : base(p =>
        (string.IsNullOrWhiteSpace(parameters.Search) || p.Name.ToLower().Contains(parameters.Search)) &&
        (!parameters.Brands.Any() || parameters.Brands.Contains(p.Brand)) &&
        (!parameters.Types.Any() || parameters.Types.Contains(p.Type)))
    {
        // ApplyPaging(parameters.PageSize, seekValue: );
        ApplyPaging(parameters.PageSize, parameters.PageSize * (parameters.PageIndex - 1));

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
