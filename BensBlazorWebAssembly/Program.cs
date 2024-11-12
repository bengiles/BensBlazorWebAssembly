using BensBlazorWebAssembly.Client.Services;
using BensBlazorWebAssembly.Components;
using BensBlazorWebAssembly.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BensBlazorWebAssembly.Client.Api;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var environment = builder.Environment;

//  ADD HTTP CLIENT
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7093/") });


// Register the VideoGameService
builder.Services.AddScoped<IRestSharpHelper, RestSharpHelper>();
builder.Services.AddScoped<IVideoGameService, VideoGameService>();

// Add Controller Services
builder.Services.AddControllers();
builder.Services.AddApiVersioning();


// Register the Db context
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BensBlazorWebAssembly.Client._Imports).Assembly);

app.Run();
