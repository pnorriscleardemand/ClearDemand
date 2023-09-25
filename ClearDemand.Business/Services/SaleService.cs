using AutoMapper;
using ClearDemand.Business.Contracts;
using ClearDemand.Data;
using ClearDemand.Shared.Models.EntityFrameworkModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClearDemand.Business.Services;

public class SaleService : ISaleService
{
    private readonly ClearDemandContext _context; // database context
    private readonly ILogger<SaleService> _logger;
    private readonly IMapper _mapper;
    private readonly IMarkdownPlanService _markdownPlanService;
    private readonly IProductService _productService;

    public SaleService(
        ClearDemandContext context,
        IMarkdownPlanService markdownPlanService,
        IProductService productService,
        ILogger<SaleService> logger,
        IMapper mapper
    )
    {
        _context = context;
        _markdownPlanService = markdownPlanService;
        _productService = productService;
        _logger = logger;
        _mapper = mapper;
    }

    // get a list of all sales
    public async Task<IEnumerable<Sale>> Get()
    {
        var sales = await _context.Sales
            .Include(b => b.Customer)
            .ToListAsync();
        return sales;
    }

    // get a single sale
    public async Task<Sale?> Get(int id)
    {
        var sale = await _context.Sales
            .Include(b => b.SaleItems)
            .FirstOrDefaultAsync(x => x.SaleId == id);

        return sale;
    }

    // get a list of all sales by product id
    public async Task<IEnumerable<Sale>> GetByProduct(int productId)
    {
        var sales = await _context.Sales
            .Include(b => b.SaleItems)
            .Where(p => p.SaleItems.Any(c => c.ProductId >= productId))
            .ToListAsync();
        return sales;
    }

    // create a new sale
    public async Task<Sale> Create(Sale sale)
    {
        ValidateModel(sale);

        // Calculate the sale items subtotal
        foreach (var saleItem in sale.SaleItems) await CalculateSubtotal(sale, saleItem);

        // Calculate the total amount.
        sale.TotalAmount = sale.SaleItems.Sum(item => item.Subtotal);

        // Decrement the inventory.
        foreach (var saleSaleItem in sale.SaleItems)
        {
            var product = await _productService.Get(saleSaleItem.ProductId);
            product!.QuantityInStock = product!.QuantityInStock - saleSaleItem.QuantitySold;
        }

        // This is all a uow, so decrement wont be an issue
        _context.Add(sale);
        await _context.SaveChangesAsync();

        return sale;
    }

    // update a sale
    public async Task<Sale> Update(Sale sale)
    {
        ValidateModel(sale);

        return null;
    }


    // delete a sale
    public bool Delete(int id)
    {
        var sale = _context.Sales.FirstOrDefault(x => x.SaleId == id);

        if (sale != null)
        {
            _context.Remove(sale);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    private async Task CalculateSubtotal(Sale sale, SaleItem saleItem)
    {
        var markdownPlan = await _markdownPlanService.GetByProductAndDate(saleItem.ProductId, sale.SaleDate);
        var product = await _productService.Get(saleItem.ProductId);

        if (markdownPlan != null)
        {
            var unitPrice = GetDiscountedPrice(sale, markdownPlan, product?.Price);
            saleItem.MarkdownPercentage = GetDiscountPercent(sale, markdownPlan, product?.Price);
            saleItem.UnitPrice = unitPrice;
            saleItem.Subtotal = unitPrice * saleItem.QuantitySold;
        }
        else
        {
            saleItem.MarkdownPercentage = 0;
            saleItem.UnitPrice = product?.Price;
            saleItem.Subtotal = product?.Price * saleItem.QuantitySold;
        }
    }

    private static decimal? GetDiscountedPrice(Sale sale, MarkdownPlan markdownPlan, decimal? price)
    {
        // Calculate the running total of discount percentages using LINQ
        var runningTotal = markdownPlan.Markdowns!.Select((markdown, index) => new
        {
            markdown.StartDate,
            markdown.EndDate,
            RunningTotal = markdownPlan.Markdowns!.Take(index + 1).Sum(item => item.MarkdownPercentage)
        }).ToList();

        // Find the discount for this sale
        var discount = runningTotal
            .FirstOrDefault(r => sale.SaleDate >= r.StartDate && sale.SaleDate <= r.EndDate)!
            .RunningTotal;

        var unitPrice = price - price * discount / 100;

        return unitPrice;
    }

    private static decimal? GetDiscountPercent(Sale sale, MarkdownPlan markdownPlan, decimal? price)
    {
        // Calculate the running total of discount percentages using LINQ
        var runningTotal = markdownPlan.Markdowns!.Select((markdown, index) => new
        {
            markdown.StartDate,
            markdown.EndDate,
            RunningTotal = markdownPlan.Markdowns!.Take(index + 1).Sum(item => item.MarkdownPercentage)
        }).ToList();

        // Find the discount for this sale
        var discount = runningTotal
            .FirstOrDefault(r => sale.SaleDate >= r.StartDate && sale.SaleDate <= r.EndDate)!
            .RunningTotal;

        return discount;
    }

    private void ValidateModel(Sale sale)
    {
        //throw new NotImplementedException();
    }
}