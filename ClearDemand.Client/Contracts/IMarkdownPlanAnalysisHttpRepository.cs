using ClearDemand.Shared.Models.ApiModel;

namespace ClearDemand.Client.Contracts;

public interface IMarkdownPlanAnalysisHttpRepository
{
    Task<MarkdownPlanDetailApiModel?> Get(string? productId);
}