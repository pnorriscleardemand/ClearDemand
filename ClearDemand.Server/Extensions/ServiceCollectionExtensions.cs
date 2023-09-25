using ClearDemand.Business.Contracts;
using ClearDemand.Business.Services;

namespace ClearDemand.Server.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IMarkdownPlanService, MarkdownPlanService>();
        services.AddTransient<IMarkdownPlanAnalysisService, MarkdownPlanAnalysisService>();
        services.AddTransient<ISalesAnalysisService, SalesAnalysisService>();
        services.AddTransient<ISaleService, SaleService>();
        services.AddAutoMapper(typeof(Program));
    }

    public static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
    {
        //services.Configure<sectionObj>(config.GetSection("section"));
    }
}