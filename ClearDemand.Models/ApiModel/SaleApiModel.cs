namespace ClearDemand.Shared.Models.ApiModel;

public class SaleApiModel
{
    public int SaleId { get; set; }

    public int? CustomerId { get; set; }

    public DateOnly? SaleDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual string? CustomerEmail { get; set; }

    public virtual ICollection<SaleItemApiModel> SaleItems { get; set; } = new List<SaleItemApiModel>();
}