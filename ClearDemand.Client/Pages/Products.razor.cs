using AutoMapper;
using ClearDemand.Client.Contracts;
using ClearDemand.Client.ViewModel;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Pages;

public partial class Products
{
    protected List<ProductViewModel>? ViewModel;
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IMapper Mapper { get; set; }
    [Inject] public required IProductHttpRepository ProductService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var apiModel = await ProductService.Get();

        ViewModel = Mapper.Map<List<ProductViewModel>>(apiModel);
    }

    private void AddProduct()
    {
        NavigationManager.NavigateTo("/addproduct");
    }
}