using System.Net.Http.Json;
using System.Text.Json;
using ClearDemand.Client.Contracts;
using ClearDemand.Shared.Models.ApiModel;

namespace ClearDemand.Client.Repositories;

public class SalesAnalysisHttpRepository : ISalesAnalysisHttpRepository
{
    private readonly HttpClient _client;
    private readonly ILogger<SalesAnalysisHttpRepository> _logger;
    private readonly JsonSerializerOptions _options;

    public SalesAnalysisHttpRepository(HttpClient client, ILogger<SalesAnalysisHttpRepository> logger)
    {
        _client = client;
        _logger = logger;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<DailySalesAnalysisApiModel>> GetByProductId(string? productId)
    {
        var response =
            await _client.GetFromJsonAsync<List<DailySalesAnalysisApiModel>>($"salesanalysis?ProductId={productId}");

        return response == null ? throw new ApplicationException("No response!") : response;
    }
}