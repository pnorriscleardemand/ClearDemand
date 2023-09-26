using ClearDemand.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Components;

public partial class MarkdownPlansListComponent : ComponentBase
{
    [Parameter] public required List<MarkdownPlanViewModel> MarkdownPlanList { get; set; }

    [Inject] public NavigationManager? NavigationManager { get; set; }

    protected void AddMarkdownPlan()
    {
        NavigationManager?.NavigateTo("/addMarkdownPlan");
    }

    protected void ViewMarkdownPlan(int markdownPlanId)
    {
        NavigationManager?.NavigateTo($"/editmarkdownplan/{markdownPlanId}");
    }

    protected void ViewMarkdownPlanAnalysis(int markdownPlanId)
    {
        NavigationManager?.NavigateTo($"/markdownplananalysis/{markdownPlanId}");
    }

    protected void ViewMarkdownPlansByProduct(int productId)
    {
        NavigationManager?.NavigateTo($"/markdownplans/{productId}");
    }
}