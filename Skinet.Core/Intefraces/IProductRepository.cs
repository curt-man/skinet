using System;
using Skinet.Core.Entities;

namespace Skinet.Core.Intefraces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(
        string? brands,
        string? type,
        string? sort);
    Task<Product?> GetProductByIdAsync(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);

    Task<IReadOnlyList<string>> GetBrandsAsync();
    Task<IReadOnlyList<string>> GetTypesAsync();

    Task<bool> SaveChangesAsync(); 
}
