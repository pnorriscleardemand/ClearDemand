using ClearDemand.Client;
using ClearDemand.Client.Components;
using ClearDemand.Client.Contracts;
using ClearDemand.Client.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<ProductListComponent>();
builder.Services.AddTransient<MarkdownPlansListComponent>();
builder.Services.AddScoped<IProductHttpRepository, ProductHttpRepository>();
builder.Services.AddScoped<IMarkdownPlanHttpRepository, MarkdownPlanHttpRepository>();
builder.Services.AddScoped<IMarkdownPlanAnalysisHttpRepository, MarkdownPlanAnalysisHttpRepository>();
builder.Services.AddScoped<ISalesHttpRepository, SalesHttpRepository>();
builder.Services.AddScoped<ISalesAnalysisHttpRepository, SalesAnalysisHttpRepository>();
builder.Services.AddAutoMapper(typeof(Program));

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

await builder.Build().RunAsync();