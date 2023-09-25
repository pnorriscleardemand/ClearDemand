using AutoMapper;
using ClearDemand.Business.Contracts;
using ClearDemand.Data;
using ClearDemand.Shared.Models.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClearDemand.Business.Services;

public class SalesAnalysisService : ISalesAnalysisService
{
    private readonly ClearDemandContext _context; // database context
    private readonly ILogger<SalesAnalysisService> _logger;
    private readonly IMapper _mapper;
    private readonly IMarkdownPlanService _markdownPlanService;
    private readonly IProductService _productService;

    public SalesAnalysisService(
        ClearDemandContext context,
        IMarkdownPlanService markdownPlanService,
        IProductService productService,
        ILogger<SalesAnalysisService> logger,
        IMapper mapper
    )
    {
        _context = context;
        _markdownPlanService = markdownPlanService;
        _productService = productService;
        _logger = logger;
        _mapper = mapper;
    }

    // get a list of all markdown plans
    public async Task<List<DailySalesAnalysis>> GetSalesAnalysisForProduct(int productId)
    {
        // Get the sales data.
        var salesData = await _context.Sales
            .Include(b => b.SaleItems)
            .ThenInclude(i => i.Product)
            .Where(p => p.SaleItems.Any(c => c.ProductId == productId)).ToListAsync();

        var dailySalesAnalysis = salesData
            .SelectMany(parent => parent.SaleItems, (parent, child) => new DailySalesAnalysis
            {
                Date = child.Sale?.SaleDate,
                ProductName = child.Product?.ProductName,
                QuantitySold = child.QuantitySold,
                Cost = child.Product?.Cost,
                SalePrice = child.UnitPrice,
                Margin = child.UnitPrice - child.Product?.Cost,
                Subtotal = child.UnitPrice * child.QuantitySold,
                CostSubtotal = child.Product?.Cost * child.QuantitySold,
                Profit = child.UnitPrice * child.QuantitySold -
                         child.Product?.Cost * child.QuantitySold // Subtotal - Cost Subtotal
            })
            .ToList();

        return dailySalesAnalysis;
    }
}