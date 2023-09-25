namespace ClearDemand.Shared.Models.ApiModel;

public class MarkdownPlanApiModel
{
    public int MarkdownPlanId { get; set; }

    public int? ProductId { get; set; }

    public string? PlanName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? MarkdownPercentage { get; set; }

    public string? PlanDescription { get; set; }
}