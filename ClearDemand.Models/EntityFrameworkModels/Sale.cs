namespace ClearDemand.Models.EntityFrameworkModels;

public class Sale
{
    public int SaleId { get; set; }

    public int? CustomerId { get; set; }

    public DateOnly? SaleDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}