using ClearDemand.Shared.ApiModel;

namespace ClearDemand.Client.Contracts;

public interface IMarkdownPlanHttpRepository
{
    Task<List<MarkdownPlanApiModel>> Get();
    Task<List<MarkdownPlanApiModel>> GetByProductId(string? productId);
    Task<MarkdownPlanDetailApiModel?> Get(string? markdownPlanId);
    Task<bool> Update(MarkdownPlanDetailApiModel markdownPlanDetailApiModel);
    Task<bool> Create(MarkdownPlanDetailApiModel markdownPlanDetailApiModel);
}