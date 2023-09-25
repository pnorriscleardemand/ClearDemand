using ClearDemand.Client.ViewModel;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Components;

public partial class MarkdownEditComponent : ComponentBase
{
    [Parameter] public required MarkdownViewModel Markdown { get; set; }

    [Inject] public NavigationManager? NavigationManager { get; set; }

    //protected void AddProduct()
    //{
    //    NavigationManager?.NavigateTo("/addproduct");
    //}

    //protected void ViewProduct(int productId)
    //{
    //    NavigationManager?.NavigateTo($"/product/{productId}");
    //}

    //protected void ViewMarkdownPlans(int productId)
    //{
    //    NavigationManager?.NavigateTo($"/markdownplans/{productId}");
    //}
}