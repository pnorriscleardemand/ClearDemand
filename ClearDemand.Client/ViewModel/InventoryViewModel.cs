﻿namespace ClearDemand.Client.ViewModel;

public class InventoryViewModel
{
    public int InventoryId { get; set; }

    public int? ProductId { get; set; }

    public int? QuantityInStock { get; set; }

    public int? ReorderLevel { get; set; }

    public int? SupplierId { get; set; }
}