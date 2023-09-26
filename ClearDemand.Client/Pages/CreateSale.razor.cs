using AutoMapper;
using ClearDemand.Client.Contracts;
using ClearDemand.Client.ViewModel;
using ClearDemand.Shared.ApiModel;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Pages;

public partial class CreateSale
{
    private int currentCount = 0;
    protected List<ProductViewModel>? ProductsViewModel;


    protected SaleViewModel? SaleViewModel;

    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Inject] public required IMapper Mapper { get; set; }
    [Inject] public required IProductHttpRepository ProductService { get; set; }

    [Inject] public required ISalesHttpRepository SalesService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // Get product list for dropdown
        var products = await ProductService.Get();
        ProductsViewModel = Mapper.Map<List<ProductViewModel>>(products);

        // Init view model
        SaleViewModel = new SaleViewModel
        {
            CustomerEmail = "",
            SaleDate = DateTime.Now.ToShortDateString(),
            SaleItems = new List<SaleItemViewModel>
            {
                new() { ProductId = 1 }
            }
        };
    }

    private void AddSaleItem()
    {
        // Add a new order item to the order
        SaleViewModel?.SaleItems.Add(new SaleItemViewModel());
    }


    private async Task Save()
    {
        // Create api model
        var apiModel = new SaleApiModel
        {
            CustomerEmail = SaleViewModel?.CustomerEmail,
            SaleDate = DateOnly.Parse(SaleViewModel?.SaleDate ?? string.Empty)
        };

        foreach (var saleItem in SaleViewModel?.SaleItems!)
            apiModel.SaleItems.Add(new SaleItemApiModel
            {
                ProductId = saleItem.ProductId,
                QuantitySold = saleItem.QuantitySold,
                UnitPrice = saleItem.UnitPrice,
                Subtotal = saleItem.QuantitySold * saleItem.UnitPrice
            });

        var result = await SalesService.Create(apiModel!);

        NavigationManager.NavigateTo("/sales");
    }
}