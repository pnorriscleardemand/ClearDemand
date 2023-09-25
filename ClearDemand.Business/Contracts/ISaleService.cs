using ClearDemand.Shared.Models.EntityFrameworkModels;

namespace ClearDemand.Business.Contracts;

public interface ISaleService
{
    Task<IEnumerable<Sale>> Get();
    Task<Sale?> Get(int id);
    Task<IEnumerable<Sale>> GetByProduct(int productId);
    Task<Sale> Create(Sale sale);
    Task<Sale?> Update(Sale sale);
    bool Delete(int id);
}