﻿namespace ClearDemand.Client.ViewModels;

public class DailySalesAnalysisViewModel
{
    public string? Date { get; set; }
    public string? ProductName { get; set; }
    public decimal? Cost { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal? Margin { get; set; }
    public decimal? Profit { get; set; }
    public decimal? Subtotal { get; set; }
    public decimal? CostSubtotal { get; set; }
    public int? QuantitySold { get; set; }
}