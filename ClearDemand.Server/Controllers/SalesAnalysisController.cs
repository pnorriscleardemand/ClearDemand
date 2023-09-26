using AutoMapper;
using ClearDemand.Business.Contracts;
using ClearDemand.Shared.ApiModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClearDemand.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesAnalysisController : ControllerBase
{
    private readonly ILogger<SalesAnalysisController> _logger;
    private readonly IMapper _mapper;
    private readonly ISalesAnalysisService _salesAnalysisService; // injecting the product service
    private readonly ISaleService _saleService; // injecting the product service

    // Dependency Injection with constructor method
    public SalesAnalysisController(
        ISaleService saleService,
        ISalesAnalysisService salesAnalysisService,
        ILogger<SalesAnalysisController> logger,
        IMapper mapper)
    {
        _saleService = saleService;
        _salesAnalysisService = salesAnalysisService;
        _logger = logger;
        _mapper = mapper;
    }

    // GET: api/<SalesAnalysisController>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string productId = "")
    {
        _logger.LogInformation("MarkdownPlans api visited at {DT}",
            DateTime.UtcNow.ToLongTimeString());

        var dailySalesAnalysis = await _salesAnalysisService.GetSalesAnalysisForProduct(Convert.ToInt32(productId));

        // Create api model
        var dailySalesAnalysisApiModel = _mapper.Map<List<DailySalesAnalysisApiModel>>(dailySalesAnalysis);

        return Ok(dailySalesAnalysisApiModel); // result;
    }
}