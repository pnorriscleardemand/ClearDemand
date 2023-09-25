using ClearDemand.Shared.Models.ApiModel;

namespace ClearDemand.Client.Contracts;

public interface ISalesAnalysisHttpRepository
{
    Task<List<DailySalesAnalysisApiModel>> GetByProductId(string? productId);
}