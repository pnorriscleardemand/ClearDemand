using ClearDemand.Shared.Models.EntityFrameworkModels;

namespace ClearDemand.Shared.Models.ApiModel;

public class MarkdownPlanDetailApiModel
{
    public int MarkdownPlanId { get; set; }

    public int? ProductId { get; set; }

    public string? PlanName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? MarkdownPercentage { get; set; }

    public string? PlanDescription { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ICollection<MarkdownApiModel> Markdowns { get; set; } = new List<MarkdownApiModel>();
}