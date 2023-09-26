using ClearDemand.Shared.ApiModel;

namespace ClearDemand.Client.Contracts;

public interface ISalesHttpRepository
{
    Task<List<SaleApiModel>> Get();
    Task<SaleApiModel?> Get(string? saleId);
    Task<List<SaleApiModel>> GetByProductId(string? productId);
    Task<bool> Create(SaleApiModel sale);
}