using ClearDemand.Shared.Models.ApiModel;

namespace ClearDemand.Client.Contracts;

public interface IProductHttpRepository
{
    Task<List<ProductApiModel>> Get();
}