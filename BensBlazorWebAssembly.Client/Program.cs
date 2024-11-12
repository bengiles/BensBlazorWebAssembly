using BensBlazorWebAssembly.Client.Api;
using BensBlazorWebAssembly.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//add http client
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// register the VideoGameService
builder.Services.AddScoped<IRestSharpHelper, RestSharpHelper>();
builder.Services.AddScoped<IVideoGameService, VideoGameService>();


await builder.Build().RunAsync();