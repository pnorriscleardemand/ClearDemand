using AutoMapper;
using ClearDemand.Client.Contracts;
using ClearDemand.Client.ViewModel;
using ClearDemand.Shared.ApiModel;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Pages;

public partial class CreateMarkdownPlan
{
    protected MarkdownPlanDetailViewModel? ViewModel;
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required HttpClient HttpClient { get; set; }
    [Inject] public required IMapper Mapper { get; set; }
    [Inject] private ILogger<EditMarkdownPlan> Logger { get; set; } = null!;
    [Inject] public required IMarkdownPlanHttpRepository MarkdownPlanService { get; set; }

    [Parameter] public string? Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ViewModel = new MarkdownPlanDetailViewModel();

        InitModel();
    }

    private void InitModel()
    {
        // Set up the model 
        ViewModel?.Markdowns.Add(new MarkdownViewModel
        {
            ProductId = Convert.ToInt32(Id)
        });
        ViewModel?.Markdowns.Add(new MarkdownViewModel
        {
            ProductId = Convert.ToInt32(Id)
        });
        ViewModel?.Markdowns.Add(new MarkdownViewModel
        {
            ProductId = Convert.ToInt32(Id)
        });
    }

    private void UpdateModel()
    {
        ViewModel!.ProductId = Convert.ToInt32(Id);

        // Update the model using the simple UI
        ViewModel!.Markdowns[0]!.StartDate = ViewModel.StartDate;
        ViewModel!.Markdowns[0]!.EndDate = ViewModel.MidDate?.AddDays(-1);
        ViewModel!.Markdowns[0]!.MarkdownPercentage = ViewModel.StartPercentage;

        ViewModel!.Markdowns[1]!.StartDate = ViewModel.MidDate;
        ViewModel!.Markdowns[1]!.EndDate = ViewModel.EndDate?.AddDays(-1);
        ViewModel!.Markdowns[1]!.MarkdownPercentage = ViewModel.MidPercentage;

        ViewModel!.Markdowns[2]!.StartDate = ViewModel.EndDate;
        ViewModel!.Markdowns[2]!.EndDate = ViewModel.EndDate;
        ViewModel!.Markdowns[2]!.MarkdownPercentage = ViewModel.EndPercentage;
    }


    private void AddMarkdownPlan()
    {
        NavigationManager.NavigateTo("/addmarkdownplan");
    }

    private async Task Submit()
    {
        // Update the markdowns list using the simple display
        UpdateModel();

        var apiModel = Mapper.Map<MarkdownPlanDetailApiModel?>(ViewModel);

        var result = await MarkdownPlanService.Create(apiModel!);

        Logger.LogInformation(@"Item saved.");

        NavigationManager.NavigateTo($"/markdownplans/{ViewModel!.ProductId}");
    }
}