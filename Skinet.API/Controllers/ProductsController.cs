using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.API.RequestHelpers;
using Skinet.Core.Entities;
using Skinet.Core.Intefraces;
using Skinet.Core.Specifications;
using Skinet.Infrastructure.Data;

namespace Skinet.API.Controllers;

public class ProductsController(IGenericRepository<Product> productRepository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] ProductSpecificationParameters parameters)
    {
        var specification = new ProductSpecification(parameters);

        return await CreatePagedResult(productRepository, specification, parameters.PageIndex, parameters.PageSize);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        productRepository.Add(product);
        if (await productRepository.SaveAllAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        return BadRequest("Problem creating a product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
        {
            return BadRequest("Cannot Update this product");
        }

        productRepository.Update(product);
        if (await productRepository.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem updating a product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        productRepository.Remove(product);

        if (await productRepository.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem deleting a product");
    }


    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<string>>> GetBrands()
    {
        var specification = new BrandSpecification();
        var brands = await productRepository.ListAsync<string>(specification);
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<string>>> GetTypes()
    {
        var specification = new TypeSpecification();
        var types = await productRepository.ListAsync<string>(specification);
        return Ok(types);
    }

    private bool ProductExists(int id)
    {
        return productRepository.Exists(id);
    }
}
