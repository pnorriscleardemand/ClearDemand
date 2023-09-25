namespace ClearDemand.Client.ViewModel;

public class ProductViewModel
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? CategoryId { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? UPC { get; set; }

    public virtual string? CategoryName { get; set; }

    public int? QuantityInStock { get; set; }

    public virtual ICollection<MarkdownPlanViewModel> MarkdownPlans { get; set; } = new List<MarkdownPlanViewModel>();

    public virtual ICollection<MarkdownViewModel> Markdowns { get; set; } = new List<MarkdownViewModel>();
}