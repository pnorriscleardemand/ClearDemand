using ClearDemand.Business.Contracts;
using ClearDemand.Data.EntityFramework;
using ClearDemand.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ClearDemand.Business.Services;

public class MarkdownPlanAnalysisService : IMarkdownPlanAnalysisService
{
    private readonly ClearDemandContext _context; // database context

    public MarkdownPlanAnalysisService(
        ClearDemandContext context
    )
    {
        _context = context;
    }

    // get a list of all markdown plans
    public async Task<List<Sale>> GetSalesAnalysisForProduct(int productId)
    {
        var salesData = await _context.Sales
            .Include(b => b.SaleItems)
            .Where(p => p.SaleItems.Any(c => c.ProductId == productId))
            .ToListAsync();

        var pricingData = await _context.MarkdownPlans
            .Include(b => b.Markdowns)
            .Where(p => p.ProductId == productId)
            .ToListAsync();

        return salesData;
    }
}