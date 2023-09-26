namespace ClearDemand.Models.EntityFramework;

public class Markdown
{
    public int MarkdownId { get; set; }

    public int? MarkdownPlanId { get; set; }

    public int? ProductId { get; set; }

    public decimal? MarkdownPercentage { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Reason { get; set; }

    public virtual Product? Product { get; set; }
    public virtual MarkdownPlan? MarkdownPlan { get; set; }
}