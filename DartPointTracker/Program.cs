using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DartPointTracker;
using DartPointTracker.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddHttpClient<IPlayerService, PlayerService>(client =>
{
    client.BaseAddress = new Uri("https://app-dartsmaster-dev-nsure-01.azurewebsites.net");
});
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

