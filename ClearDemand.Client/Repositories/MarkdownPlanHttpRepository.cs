using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ClearDemand.Client.Contracts;
using ClearDemand.Shared.Models.ApiModel;
using Newtonsoft.Json;

namespace ClearDemand.Client.Repositories;

public class MarkdownPlanHttpRepository : IMarkdownPlanHttpRepository
{
    private readonly HttpClient _client;
    private readonly ILogger<MarkdownPlanHttpRepository> _logger;
    private readonly JsonSerializerOptions _options;

    public MarkdownPlanHttpRepository(HttpClient client, ILogger<MarkdownPlanHttpRepository> logger)
    {
        _client = client;
        _logger = logger;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<MarkdownPlanApiModel>> Get()
    {
        var response = await _client.GetFromJsonAsync<MarkdownPlanApiModel[]>("MarkdownPlans");

        return response == null ? throw new ApplicationException("No response!") : response.ToList();
    }

    public async Task<MarkdownPlanDetailApiModel?> Get(string? markdownPlanId)
    {
        var response = await _client.GetFromJsonAsync<MarkdownPlanDetailApiModel>($"MarkdownPlans/{markdownPlanId}");

        return response;
    }

    public async Task<List<MarkdownPlanApiModel>> GetByProductId(string? productId)
    {
        var response = await _client.GetFromJsonAsync<MarkdownPlanApiModel[]>($"MarkdownPlans?ProductId={productId}");

        return response == null ? throw new ApplicationException("No response!") : response.ToList();
    }

    public async Task<bool> Update(MarkdownPlanDetailApiModel markdownPlanDetailApiModel)
    {
        // Convert the payload to a StringContent
        var content = new StringContent(JsonConvert.SerializeObject(markdownPlanDetailApiModel), Encoding.UTF8,
            "application/json");

        var response = await _client.PutAsync($"MarkdownPlans/{markdownPlanDetailApiModel.MarkdownPlanId}", content);

        // Check if the request was successful
        if (response.IsSuccessStatusCode) return true;

        _logger.LogInformation($"POST request was unsuccessful.{response.StatusCode}");

        return false;
    }

    public async Task<bool> Create(MarkdownPlanDetailApiModel markdownPlanDetailApiModel)
    {
        // Convert the payload to a StringContent
        var content = new StringContent(JsonConvert.SerializeObject(markdownPlanDetailApiModel), Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("MarkdownPlans", content);

        // Check if the request was successful
        if (response.IsSuccessStatusCode) return true;

        _logger.LogInformation($"POST request was unsuccessful.{response.StatusCode}");

        return false;
    }
}