namespace ClearDemand.Shared.ApiModel;

public class SaleItemApiModel
{
    public int SaleItemId { get; set; }

    public int? SaleId { get; set; }

    public int? ProductId { get; set; }

    public int? QuantitySold { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual ProductApiModel? Product { get; set; }

    public virtual SaleApiModel? Sale { get; set; }
}