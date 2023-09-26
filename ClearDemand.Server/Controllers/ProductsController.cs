using AutoMapper;
using ClearDemand.Business.Contracts;
using ClearDemand.Shared.ApiModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClearDemand.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IMapper _mapper;
    private readonly IProductService _productService; // injecting the product service

    // Dependency Injection with constructor method
    public ProductsController(
        IProductService productService,
        ILogger<ProductsController> logger,
        IMapper mapper)
    {
        _productService = productService;
        _logger = logger;
        _mapper = mapper;
    }

    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Products page visited at {DT}",
            DateTime.UtcNow.ToLongTimeString());

        var products = await _productService.Get();

        var apiModel = _mapper.Map<List<ProductApiModel>>(products);

        return Ok(apiModel);
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ProductsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}