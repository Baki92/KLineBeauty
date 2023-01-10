using CustomerManager.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CustomerManager.Web.Services;
using CustomerManager.Web.Services.Interface;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<Radzen.NotificationService>();
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<CustomerService>();
await builder.Build().RunAsync();
