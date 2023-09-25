namespace ClearDemand.Shared.Models.EntityFrameworkModels;

public class MarkdownPlan
{
    public int MarkdownPlanId { get; set; }

    public int? ProductId { get; set; }

    public string? PlanName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? MarkdownPercentage { get; set; }

    public string? PlanDescription { get; set; }

    public virtual Product? Product { get; set; }

    public virtual List<Markdown>? Markdowns { get; set; } = new List<Markdown>();
}