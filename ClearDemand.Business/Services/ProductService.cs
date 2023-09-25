using ClearDemand.Business.Contracts;
using ClearDemand.Data;
using ClearDemand.Shared.Models.EntityFrameworkModels;
using Microsoft.EntityFrameworkCore;

namespace ClearDemand.Business.Services;

public class ProductService : IProductService
{
    private readonly ClearDemandContext _context; // database context

    public ProductService(
        ClearDemandContext context
    )
    {
        _context = context;
    }

    // get a list of all products
    public async Task<IEnumerable<Product>> Get()
    {
        var products = await _context.Products
            .Include(b => b.Category)
            .Include(b => b.Inventories)
            .ToListAsync();
        return products;
    }

    // get a single product
    public async Task<Product?> Get(int? id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

        return product;
    }

    // create a new product
    public Product Create(Product product)
    {
        ValidateModel(product);

        _context.Add(product);
        _context.SaveChanges();

        return product;
    }


    // delete a product
    public bool Delete(int id)
    {
        var product = _context.Products.FirstOrDefault(x => x.ProductId == id);

        if (product != null)
        {
            _context.Remove(product);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    private void ValidateModel(Product markdownPlan)
    {
        //throw new NotImplementedException();
    }
}