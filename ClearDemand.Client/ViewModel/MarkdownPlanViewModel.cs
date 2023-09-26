using System.ComponentModel.DataAnnotations;
using ClearDemand.Shared.ApiModel;

namespace ClearDemand.Client.ViewModel;

public class MarkdownPlanViewModel
{
    public int MarkdownPlanId { get; set; }

    public int? ProductId { get; set; }
    [Required]
    public string? PlanName { get; set; }
    [Required]
    public DateOnly? StartDate { get; set; }
    [Required]
    public DateOnly? EndDate { get; set; }
    public string? PlanDescription { get; set; }
    public virtual List<MarkdownApiModel> Markdowns { get; set; } = new();
}