using AutoMapper;
using ClearDemand.Client.Contracts;
using ClearDemand.Client.ViewModel;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Pages;

public partial class SalesAnalysis
{
    protected List<DailySalesAnalysisViewModel>? ViewModel;
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required HttpClient HttpClient { get; set; }
    [Inject] public required IMapper Mapper { get; set; }
    [Inject] public required ISalesAnalysisHttpRepository SalesAnalysisService { get; set; }
    [Parameter] public string? Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var dailySalesAnalysisApiModel = await SalesAnalysisService.GetByProductId(Id);

        ViewModel = Mapper.Map<List<DailySalesAnalysisViewModel>>(dailySalesAnalysisApiModel);
    }

    private void AddMarkdownPlan()
    {
        NavigationManager.NavigateTo("/addmarkdownplan");
    }
}