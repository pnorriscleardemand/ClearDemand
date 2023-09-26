using AutoMapper;
using ClearDemand.Client.Converters;
using ClearDemand.Client.ViewModel;
using ClearDemand.Models.Business;
using ClearDemand.Models.EntityFrameworkModels;
using ClearDemand.Shared.ApiModel;

namespace ClearDemand.Server;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
        CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());

        // Mapping from DateOnly to string
        CreateMap<DateOnly, string>().ConvertUsing(dateOnly => dateOnly.ToString());
        CreateMap<IEnumerable<DateOnly>, IEnumerable<string>>().ConvertUsing(
            (dateOnlyList, _, context) =>
                dateOnlyList.Select(dateOnly => context.Mapper.Map<DateOnly, string>(dateOnly))
        );

        // Mapping from string to DateOnly
        CreateMap<string, DateOnly>().ConvertUsing(dateOnlyString => DateOnly.Parse(dateOnlyString));
        CreateMap<IEnumerable<string>, IEnumerable<DateOnly>>().ConvertUsing(
            (dateOnlyStringList, _, context) =>
                dateOnlyStringList.Select(dateOnlyString => context.Mapper.Map<string, DateOnly>(dateOnlyString))
        );

        CreateMap<DailySalesAnalysisApiModel, DailySalesAnalysisViewModel>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString()));

        CreateMap<Product, ProductApiModel>();
        CreateMap<Product, ProductApiModel>().ReverseMap();

        CreateMap<ProductApiModel, ProductViewModel>();
        CreateMap<ProductApiModel, ProductViewModel>().ReverseMap();

        CreateMap<MarkdownPlanApiModel, MarkdownPlanViewModel>();
        CreateMap<MarkdownPlanApiModel, MarkdownPlanViewModel>().ReverseMap();

        CreateMap<MarkdownPlanDetailApiModel, MarkdownPlanDetailViewModel>();
        CreateMap<MarkdownPlanDetailApiModel, MarkdownPlanDetailViewModel>().ReverseMap();

        CreateMap<MarkdownApiModel, MarkdownViewModel>();
        CreateMap<MarkdownApiModel, MarkdownViewModel>().ReverseMap();

        CreateMap<SaleApiModel, SaleViewModel>();
        CreateMap<SaleApiModel, SaleViewModel>().ReverseMap();

        CreateMap<SaleApiModel, Sale>();
        CreateMap<SaleApiModel, Sale>().ReverseMap();

        CreateMap<SaleItemApiModel, SaleItem>();
        CreateMap<SaleItemApiModel, SaleItem>().ReverseMap();

        CreateMap<DailySalesAnalysisApiModel, DailySalesAnalysis>();
        CreateMap<DailySalesAnalysisApiModel, DailySalesAnalysis>().ReverseMap();

        CreateMap<MarkdownPlan, MarkdownPlanDetailApiModel>();
        CreateMap<MarkdownPlan, MarkdownPlanDetailApiModel>().ReverseMap();

        CreateMap<Markdown, MarkdownApiModel>();
        CreateMap<Markdown, MarkdownApiModel>().ReverseMap();

        CreateMap<MarkdownPlan, MarkdownPlanApiModel>();
        CreateMap<MarkdownPlan, MarkdownPlanApiModel>().ReverseMap();

        CreateMap<Sale, SaleApiModel>();
        CreateMap<Sale, SaleApiModel>().ReverseMap();

        CreateMap<Inventory, InventoryApiModel>();
        CreateMap<Inventory, InventoryApiModel>().ReverseMap();
    }
}