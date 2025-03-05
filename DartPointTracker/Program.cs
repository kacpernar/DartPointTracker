using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DartPointTracker;
using DartPointTracker.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddHttpClient<IPlayerService, PlayerService>(client => {
    client.BaseAddress = new Uri("https://localhost:7276");
    var credentials = Convert.ToBase64String("username:password"u8.ToArray());
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials); });
await builder.Build().RunAsync();