using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ClearDemand.Client.Contracts;
using ClearDemand.Shared.ApiModel;
using Newtonsoft.Json;

namespace ClearDemand.Client.Repositories;

public class SalesHttpRepository : ISalesHttpRepository
{
    private readonly HttpClient _client;
    private readonly ILogger<SalesHttpRepository> _logger;
    private readonly JsonSerializerOptions _options;

    public SalesHttpRepository(HttpClient client, ILogger<SalesHttpRepository> logger)
    {
        _client = client;
        _logger = logger;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<SaleApiModel>> Get()
    {
        var response = await _client.GetFromJsonAsync<SaleApiModel[]>("sales");

        return response == null ? throw new ApplicationException("No response!") : response.ToList();
    }

    public async Task<SaleApiModel?> Get(string? saleId)
    {
        var response = await _client.GetFromJsonAsync<SaleApiModel>($"sales/{saleId}");

        return response;
    }

    public async Task<List<SaleApiModel>> GetByProductId(string? productId)
    {
        var response = await _client.GetFromJsonAsync<SaleApiModel[]>($"sales?ProductId={productId}");

        return response == null ? throw new ApplicationException("No response!") : response.ToList();
    }

    public async Task<bool> Create(SaleApiModel sale)
    {
        // Convert the payload to a StringContent
        var content = new StringContent(JsonConvert.SerializeObject(sale), Encoding.UTF8,
            "application/json");

        var response = await _client.PostAsync("sales", content);

        // Check if the request was successful
        if (response.IsSuccessStatusCode) return true;

        _logger.LogInformation($"POST request was unsuccessful.{response.StatusCode}");

        return false;
    }
}