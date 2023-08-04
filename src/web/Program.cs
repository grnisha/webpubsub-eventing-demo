using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp.Client;
using Demo.WebPubSub.Web.ProdcutService;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
var baseAPIUri = builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
// file deepcode ignore Ssrf: Not an issue
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAPIUri) });

builder.Services.AddScoped<IProductService, ProdcutService>();
await builder.Build().RunAsync();
