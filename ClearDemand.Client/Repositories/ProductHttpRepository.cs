using System.Net.Http.Json;
using System.Text.Json;
using ClearDemand.Client.Contracts;
using ClearDemand.Shared.ApiModels;

namespace ClearDemand.Client.Repositories;

public class ProductHttpRepository : IProductHttpRepository
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public ProductHttpRepository(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<ProductApiModel>> Get()
    {
        var response = await _client.GetFromJsonAsync<ProductApiModel[]>("Products");

        return response == null ? throw new ApplicationException("No response!") : response.ToList();
    }
}