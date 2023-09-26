using System.Net.Http.Json;
using System.Text.Json;
using ClearDemand.Client.Contracts;
using ClearDemand.Shared.ApiModels;

namespace ClearDemand.Client.Repositories;

public class MarkdownPlanAnalysisHttpRepository : IMarkdownPlanAnalysisHttpRepository
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public MarkdownPlanAnalysisHttpRepository(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<MarkdownPlanDetailApiModel?> Get(string? productId)
    {
        var response = await _client.GetFromJsonAsync<MarkdownPlanDetailApiModel>("MarkdownPlanAnalysis/1");

        return response;
    }
}