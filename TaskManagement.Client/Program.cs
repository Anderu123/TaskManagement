using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskManagement.Client;
using TaskManagement.SharedData;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<ITaskAccessLayer, TasksAccessLayer>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7017") });
builder.Services.AddScoped<TaskApi>();
await builder.Build().RunAsync();
