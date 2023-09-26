using ClearDemand.Shared.ApiModels;

namespace ClearDemand.Client.Contracts;

public interface IMarkdownPlanAnalysisHttpRepository
{
    Task<MarkdownPlanDetailApiModel?> Get(string? productId);
}