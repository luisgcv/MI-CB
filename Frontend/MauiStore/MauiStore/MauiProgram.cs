// MauiStore/MauiProgram.cs
using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

using Microsoft.AspNetCore.Components.Authorization;     // AuthStateProvider / AddAuthorizationCore

using MauiStore.Shared.Services;                         // IFormFactor, ClientPreferenceManager, ITokenStorageService
using MauiStore.Infrastructure;                          // FormFactor
using MauiStore.Web.Services;
using MauiStore.Services;
using Blazored.LocalStorage;
using MauiStore.Infrastructure.Interfaces;
using MauiStore.Shared.Infrastructure.Interfaces;                            // JwtAuthStateProvider, AuthHeaderHandler, IAuthService, AuthService

namespace MauiStore;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(f => f.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

        // ===== Blazor WebView =====
        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // ===== UI / utilidades que usa tu Shared =====
        builder.Services.AddMudServices();
        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddScoped<ClientPreferenceManager>();
        builder.Services.AddSingleton<IFileService, MauiFileService>();

        // ===== Autorización (CLIENTE) =====
        builder.Services.AddAuthorizationCore();

        // AuthState basado en JWT
        builder.Services.AddScoped<JwtAuthStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthStateProvider>());

        // Storage de token para MAUI (usa SecureStorage)
        builder.Services.AddScoped<ITokenStorageService, MauiTokenStorageService>();
       // builder.Services.AddScoped<IPurchasePreferenceManager, PurchasePreferenceManager>();
      /*  builder.Services.AddScoped<StoresService>();
        builder.Services.AddScoped<CategoriesService>();
        builder.Services.AddScoped<ProductsService>();
        builder.Services.AddScoped<CartsService>();
        builder.Services.AddScoped<AddressesService>();
        builder.Services.AddScoped<OrdersService>();
        builder.Services.AddScoped<NewsService>();
        builder.Services.AddScoped<SorteosService>();
        builder.Services.AddScoped<ProfileService>();
        builder.Services.AddScoped<WishlistsService>();  */


#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
        Android.Webkit.WebView.SetWebContentsDebuggingEnabled(true);
#endif

        // Handler que agrega Authorization: Bearer <token>
        builder.Services.AddTransient<AuthHeaderHandler>();

        // HttpClient hacia tu API con el handler del Bearer
        builder.Services.AddHttpClient("Api", c =>
        {
            // Igual que en Web:
            c.BaseAddress = new Uri("https://fakestoreapi.com/");
        })
        .AddHttpMessageHandler<AuthHeaderHandler>();


        builder.Services.AddBlazoredLocalStorage();
        // HttpClient por defecto = cliente "Api"
        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

        // Servicio de login/logout
        builder.Services.AddScoped<IAuthService, AuthService>();
       // builder.Services.AddScoped<IAddressService, AddressService>();

        return builder.Build();
    }
}

