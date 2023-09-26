namespace ClearDemand.Models.EntityFrameworkModels;

public class Inventory
{
    public int InventoryId { get; set; }

    public int? ProductId { get; set; }

    public int? QuantityInStock { get; set; }

    public int? ReorderLevel { get; set; }

    public int? SupplierId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Supplier? Supplier { get; set; }
}