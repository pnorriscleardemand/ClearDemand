using System.ComponentModel.DataAnnotations;

namespace ClearDemand.Client.ViewModel;

public class MarkdownViewModel
{
    public int MarkdownId { get; set; }

    public int? MarkdownPlanId { get; set; }

    public int? ProductId { get; set; }
    [Required]
    public decimal? MarkdownPercentage { get; set; }
    [Required]
    public DateOnly? StartDate { get; set; }
    [Required]
    public DateOnly? EndDate { get; set; }

    public string? Reason { get; set; }
}