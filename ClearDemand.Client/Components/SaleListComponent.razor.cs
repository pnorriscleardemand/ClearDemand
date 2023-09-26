using ClearDemand.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Components;

public partial class SaleListComponent : ComponentBase
{
    [Parameter] public required List<SaleViewModel> Sales { get; set; }

    [Inject] public NavigationManager? NavigationManager { get; set; } = default;

    protected void AddSale()
    {
        NavigationManager?.NavigateTo("/addsale");
    }

    protected void ViewSale(int productId)
    {
        NavigationManager?.NavigateTo($"/sale/{productId}");
    }

    protected void ViewSales(int productId)
    {
        NavigationManager?.NavigateTo($"/sales/{productId}");
    }
}