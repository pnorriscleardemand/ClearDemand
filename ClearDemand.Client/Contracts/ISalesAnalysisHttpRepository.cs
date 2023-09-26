using ClearDemand.Shared.ApiModels;

namespace ClearDemand.Client.Contracts;

public interface ISalesAnalysisHttpRepository
{
    Task<List<DailySalesAnalysisApiModel>> GetByProductId(string? productId);
}