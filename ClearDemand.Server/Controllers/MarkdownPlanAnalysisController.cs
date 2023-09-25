using AutoMapper;
using ClearDemand.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClearDemand.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MarkdownPlanAnalysisController : ControllerBase
{
    private readonly ILogger<MarkdownPlanAnalysisController> _logger;
    private readonly IMapper _mapper;
    private readonly IProductService _productService; // injecting the product service
    private readonly IMarkdownPlanAnalysisService _salesAnalysisService;

    // Dependency Injection with constructor method
    public MarkdownPlanAnalysisController(
        IProductService productService,
        IMarkdownPlanAnalysisService salesAnalysisService,
        ILogger<MarkdownPlanAnalysisController> logger,
        IMapper mapper)
    {
        _productService = productService;
        _salesAnalysisService = salesAnalysisService;
        _logger = logger;
        _mapper = mapper;
    }

    // GET api/<MarkdownPlansController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var x = await _salesAnalysisService.GetSalesAnalysisForProduct(id);

        return Ok();
    }
}