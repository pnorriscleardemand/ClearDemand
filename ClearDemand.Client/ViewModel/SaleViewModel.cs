namespace ClearDemand.Client.ViewModel;

public class SaleViewModel
{
    public int? SaleId { get; set; }

    public int? CustomerId { get; set; }

    public string? SaleDate { get; set; }

    public string? TotalAmount { get; set; }

    public virtual string? CustomerEmail { get; set; }

    public virtual ICollection<SaleItemViewModel> SaleItems { get; set; } = new List<SaleItemViewModel>();
}