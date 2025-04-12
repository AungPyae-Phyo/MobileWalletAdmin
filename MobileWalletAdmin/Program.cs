using MudBlazor.Services;
using MobileWalletAdmin.Components;
using Microsoft.AspNetCore.Components.Authorization;
//using MobileWalletAdmin.Authentication;
using MobileWalletAdmin.Services;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<KYCService>();
builder.Services.AddScoped<WalletService>();
builder.Services.AddBlazoredLocalStorage();


//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<HttpClient>(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://localhost:7249/") };
    return client;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();