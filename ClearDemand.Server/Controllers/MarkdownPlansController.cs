using AutoMapper;
using ClearDemand.Business.Contracts;
using ClearDemand.Shared.Models.ApiModel;
using ClearDemand.Shared.Models.EntityFrameworkModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClearDemand.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MarkdownPlansController : ControllerBase
{
    private readonly ILogger<MarkdownPlansController> _logger;
    private readonly IMapper _mapper;
    private readonly IMarkdownPlanService _markdownPlanService; // injecting the product service

    // Dependency Injection with constructor method
    public MarkdownPlansController(
        IMarkdownPlanService markdownPlanService,
        ILogger<MarkdownPlansController> logger,
        IMapper mapper)
    {
        _markdownPlanService = markdownPlanService;
        _logger = logger;
        _mapper = mapper;
    }

    // GET: api/<MarkdownPlansController>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string productId = "")
    {
        _logger.LogInformation("MarkdownPlans api visited at {DT}",
            DateTime.UtcNow.ToLongTimeString());

        var markdownPlans = productId == ""
            ? await _markdownPlanService.Get()
            : await _markdownPlanService.GetByProduct(Convert.ToInt32(productId));

        var markdownPlansApiModels = _mapper.Map<List<MarkdownPlanApiModel>>(markdownPlans);

        return Ok(markdownPlansApiModels);
    }

    // GET api/<MarkdownPlansController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var markdownPlan = await _markdownPlanService.Get(id);

        if (markdownPlan == null) return NotFound();

        var markdownPlanDetailApiModel = _mapper.Map<MarkdownPlanDetailApiModel>(markdownPlan);

        return Ok(markdownPlanDetailApiModel);
    }

    // POST api/<MarkdownPlansController>
    [HttpPost]
    public async Task Post(MarkdownPlanDetailApiModel markdownPlanApiModel)
    {
        var markdownPlan = _mapper.Map<MarkdownPlan>(markdownPlanApiModel);

        await _markdownPlanService.Create(markdownPlan);
    }

    // PUT api/<MarkdownPlansController>/5
    [HttpPut("{id}")]
    public async Task Put(int id, MarkdownPlanDetailApiModel markdownPlanApiModel)
    {
        var markdownPlan = _mapper.Map<MarkdownPlan>(markdownPlanApiModel);

        await _markdownPlanService.Update(markdownPlan);
    }

    // DELETE api/<MarkdownPlansController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}