using ClearDemand.Models.EntityFrameworkModels;

namespace ClearDemand.Business.Contracts;

public interface IProductService
{
    public Task<IEnumerable<Product>> Get();
    Task<Product?> Get(int? id);
    Product Create(Product product);
    bool Delete(int id);
}