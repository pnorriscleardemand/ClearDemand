using ClearDemand.Client.ViewModel;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Components;

public partial class ProductListComponent : ComponentBase
{
    [Parameter] public required List<ProductViewModel> ProductList { get; set; }

    [Inject] public NavigationManager? NavigationManager { get; set; } = default;

    protected void AddProduct()
    {
        NavigationManager?.NavigateTo("/addproduct");
    }

    protected void ViewProduct(int productId)
    {
        NavigationManager?.NavigateTo($"/product/{productId}");
    }

    protected void ViewMarkdownPlans(int productId)
    {
        NavigationManager?.NavigateTo($"/markdownplans/{productId}");
    }

    protected void ViewSalesAnalysis(int productId)
    {
        NavigationManager?.NavigateTo($"/salesanalysis/{productId}");
    }
}