namespace ClearDemand.Models.EntityFrameworkModels;

public class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? CategoryId { get; set; }

    public decimal? Price { get; set; }
    public decimal? Cost { get; set; }

    public int? QuantityInStock { get; set; }

    public string? Description { get; set; }
    public string? UPC { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<MarkdownPlan> MarkdownPlans { get; set; } = new List<MarkdownPlan>();

    public virtual ICollection<Markdown> Markdowns { get; set; } = new List<Markdown>();

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}