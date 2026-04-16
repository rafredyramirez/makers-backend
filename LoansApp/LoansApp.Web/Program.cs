using Blazored.LocalStorage;
using LoansApp.Web;
using LoansApp.Web.Components;
using LoansApp.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuración base de API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiUrl"])
});

// Servicios
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<LoanService>();

// LocalStorage (para JWT)
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

var app = builder.Build();

await app.RunAsync();