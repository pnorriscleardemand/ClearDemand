using ClearDemand.Shared.ApiModel;

namespace ClearDemand.Client.Contracts;

public interface ISalesAnalysisHttpRepository
{
    Task<List<DailySalesAnalysisApiModel>> GetByProductId(string? productId);
}