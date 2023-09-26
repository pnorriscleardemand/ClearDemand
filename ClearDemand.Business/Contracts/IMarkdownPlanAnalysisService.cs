using ClearDemand.Models.EntityFrameworkModels;

namespace ClearDemand.Business.Contracts;

public interface IMarkdownPlanAnalysisService
{
    Task<List<Sale>> GetSalesAnalysisForProduct(int productId);
}