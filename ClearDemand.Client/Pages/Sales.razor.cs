using AutoMapper;
using ClearDemand.Client.Contracts;
using ClearDemand.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Pages;

public partial class Sales
{
    protected List<SaleViewModel>? ViewModel;
    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Inject] public required HttpClient HttpClient { get; set; }
    [Inject] public required IMapper Mapper { get; set; }
    [Inject] public required ISalesHttpRepository SalesService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var apiModel = await SalesService.Get();

        ViewModel = Mapper.Map<List<SaleViewModel>>(apiModel);
    }

    private void AddSale()
    {
        NavigationManager.NavigateTo("/createsale");
    }
}