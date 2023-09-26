namespace ClearDemand.Shared.ApiModel;

public class MarkdownApiModel
{
    public int MarkdownId { get; set; }

    public int? MarkdownPlanId { get; set; }

    public int? ProductId { get; set; }

    public decimal? MarkdownPercentage { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Reason { get; set; }
}