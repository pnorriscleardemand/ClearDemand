using AutoMapper;
using ClearDemand.Business.Contracts;
using ClearDemand.Models.EntityFrameworkModels;
using ClearDemand.Shared.ApiModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClearDemand.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesController : ControllerBase
{
    private readonly ILogger<SalesController> _logger;
    private readonly IMapper _mapper;
    private readonly ISaleService _saleService; // injecting the product service

    // Dependency Injection with constructor method
    public SalesController(
        ISaleService saleService,
        ILogger<SalesController> logger,
        IMapper mapper)
    {
        _saleService = saleService;
        _logger = logger;
        _mapper = mapper;
    }

    // GET: api/<SalesController>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string productId = "")
    {
        _logger.LogInformation("MarkdownPlans api visited at {DT}",
            DateTime.UtcNow.ToLongTimeString());

        var sales = productId == ""
            ? await _saleService.Get()
            : await _saleService.GetByProduct(Convert.ToInt32(productId));

        var saleApiModel = _mapper.Map<List<SaleApiModel>>(sales);

        return Ok(saleApiModel);
    }

    // GET api/<SalesController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var sale = await _saleService.Get(id);

        if (sale == null) return NotFound();

        var saleApiModel = _mapper.Map<List<SaleApiModel>>(sale);

        return Ok(saleApiModel);
    }

    // POST api/<SalesController>
    [HttpPost]
    public async Task Post(SaleApiModel saleApiModel)
    {
        // Create api model
        var sale = _mapper.Map<Sale>(saleApiModel);

        await _saleService.Create(sale);
    }

    // PUT api/<SalesController>/5
    [HttpPut("{id}")]
    public async Task Put(int id, SaleApiModel saleApiModel)
    {
        var sale = _mapper.Map<Sale>(saleApiModel);

        await _saleService.Update(sale);
    }

    // DELETE api/<SalesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}