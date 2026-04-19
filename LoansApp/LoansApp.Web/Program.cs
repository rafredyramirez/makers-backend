using Blazored.LocalStorage;
using LoansApp.Web;
using LoansApp.Web.Components;
using LoansApp.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiUrl = builder.Configuration.GetValue<string>("ApiUrl")
             ?? throw new InvalidOperationException("La configuración 'ApiUrl' no está definida en appsettings.json");

builder.Services.AddHttpClient("Api", client =>
{
    //client.BaseAddress = new Uri(apiUrl);
    client.BaseAddress = new Uri(apiUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<LoanService>();
builder.Services.AddScoped<AuthStateService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();