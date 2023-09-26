using ClearDemand.Shared.ApiModel;

namespace ClearDemand.Client.Contracts;

public interface IProductHttpRepository
{
    Task<List<ProductApiModel>> Get();
}