﻿using ClearDemand.Shared.Models.EntityFrameworkModels;

namespace ClearDemand.Client.ViewModel;

public class SaleItemViewModel
{
    public int SaleItemId { get; set; }

    public int? SaleId { get; set; }

    public int? ProductId { get; set; }

    public int? QuantitySold { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Sale? Sale { get; set; }
}