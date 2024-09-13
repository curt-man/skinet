using System;
using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Intefraces;

namespace Skinet.Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add( product );
    }

    public void UpdateProduct(Product product)
    {
        context.Products.Update( product );
    }
    public void DeleteProduct(Product product)
    {
        context.Products.Remove( product );
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();
        if(!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(p=>p.Brand == brand);
        }
        if(!string.IsNullOrWhiteSpace(type))
        {
            query = query.Where(p=>p.Type == type);
        }
        if(!string.IsNullOrWhiteSpace(sort))
        {
            query = sort switch
            {
                "priceAsc"=>query.OrderBy(p=>p.Price),
                "priceDesc"=>query.OrderByDescending(p=>p.Price),
                _ => query.OrderBy(p=>p.Name)
            };
        }
        return await query.ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);
    }


    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products.Select(p=>p.Brand).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(p=>p.Type).Distinct().ToListAsync();
    }

    public IReadOnlyList<int> GetIntsAsync()
    {
        return context.Products.Select(x=>x.Id).ToList();
    }


    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

}
