namespace ClearDemand.Client.ViewModels;

public class MarkdownPlanDetailViewModel
{
    public int MarkdownPlanId { get; set; }

    public int? ProductId { get; set; }

    public string? PlanName { get; set; }

    public DateOnly? StartDate { get; set; }
    public decimal? StartPercentage { get; set; }

    public DateOnly? MidDate { get; set; }
    public decimal? MidPercentage { get; set; }

    public DateOnly? EndDate { get; set; }
    public decimal? EndPercentage { get; set; }

    public decimal? MarkdownPercentage { get; set; }

    public string? PlanDescription { get; set; }

    public virtual List<MarkdownViewModel?> Markdowns { get; set; } = new();
}