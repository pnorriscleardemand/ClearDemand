using ClearDemand.Models.Business;

namespace ClearDemand.Business.Contracts;

public interface ISalesAnalysisService
{
    Task<List<DailySalesAnalysis>> GetSalesAnalysisForProduct(int productId);
}