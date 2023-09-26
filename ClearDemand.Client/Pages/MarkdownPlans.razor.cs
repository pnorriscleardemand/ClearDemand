using AutoMapper;
using ClearDemand.Client.Contracts;
using ClearDemand.Client.ViewModels;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Pages;

public partial class MarkdownPlans
{
    protected List<MarkdownPlanViewModel>? ViewModel;
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required HttpClient HttpClient { get; set; }
    [Inject] public required IMapper Mapper { get; set; }
    [Inject] public required IMarkdownPlanHttpRepository MarkdownPlanService { get; set; }
    [Parameter] public string? Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var markdownPlanApiModel = await MarkdownPlanService.GetByProductId(Id);

        ViewModel = Mapper.Map<List<MarkdownPlanViewModel>>(markdownPlanApiModel);
    }

    private void AddMarkdownPlan()
    {
        NavigationManager.NavigateTo(@$"/createmarkdownplan/{Id}");
    }
}