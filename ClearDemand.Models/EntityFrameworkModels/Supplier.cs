namespace ClearDemand.Models.EntityFrameworkModels;

public class Supplier
{
    public int SupplierId { get; set; }

    public string? SupplierName { get; set; }

    public string? ContactName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}