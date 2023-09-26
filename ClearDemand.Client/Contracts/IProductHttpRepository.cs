using ClearDemand.Shared.ApiModels;

namespace ClearDemand.Client.Contracts;

public interface IProductHttpRepository
{
    Task<List<ProductApiModel>> Get();
}