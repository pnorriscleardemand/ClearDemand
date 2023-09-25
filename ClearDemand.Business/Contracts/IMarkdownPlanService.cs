using ClearDemand.Shared.Models.EntityFrameworkModels;

namespace ClearDemand.Business.Contracts;

public interface IMarkdownPlanService
{
    Task<IEnumerable<MarkdownPlan>> Get();
    Task<IEnumerable<MarkdownPlan>> GetByProduct(int? productId);
    Task<MarkdownPlan?> Get(int id);
    Task<MarkdownPlan> Create(MarkdownPlan markdownPlan);
    bool Delete(int id);
    Task<MarkdownPlan> Update(MarkdownPlan markdownPlan);

    Task<MarkdownPlan> GetByProductAndDate(int? productId, DateOnly? date);
}