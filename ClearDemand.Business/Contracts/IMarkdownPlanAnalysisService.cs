using ClearDemand.Shared.Models.EntityFrameworkModels;

namespace ClearDemand.Business.Contracts;

public interface IMarkdownPlanAnalysisService
{
    Task<List<Sale>> GetSalesAnalysisForProduct(int productId);
}