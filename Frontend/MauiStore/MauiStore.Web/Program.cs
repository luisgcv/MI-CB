// MauiStore.Web/Program.cs
using MauiStore.Web.Components;
using MauiStore.Shared.Services;
using MauiStore.Web.Services;
using MudBlazor.Services;
using Blazored.LocalStorage;
using MauiStore.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.AspNetCore.Components.Authorization;         // AuthorizationCore / AuthStateProvider
using Microsoft.AspNetCore.Authentication.Cookies;          // CookieAuthenticationDefaults
using Microsoft.AspNetCore.Http;                            // StatusCodes
using System;
using MauiStore.Infrastructure.Interfaces;
using MauiStore.Shared.Infrastructure.Interfaces;                                               // Uri

var builder = WebApplication.CreateBuilder(args);

// Blazor Server (interactividad)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// MudBlazor
builder.Services.AddMudServices();

// (Opcional) si ya lo usas para otras cosas, puede quedarse
builder.Services.AddBlazoredLocalStorage();

// Preferencias existentes
builder.Services.AddScoped<ClientPreferenceManager>();
builder.Services.AddScoped<ProfileService>();

// Servicios específicos compartidos
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddScoped<IFileService, WebFileService>();

// ======  AUTENTICACIÓN/AUTORIZACIÓN DEL SERVIDOR (requerido por [Authorize]) ======
/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/";
        options.AccessDeniedPath = "/";

        options.Events.OnRedirectToLogin = ctx =>
        {
            if (IsApiRequest(ctx.Request))  
            {
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized; // API/AJAX -> 401
                return Task.CompletedTask;
            }

            // Navegación de página -> redirige al login con returnUrl
            var returnUrl = Uri.EscapeDataString(ctx.Request.Path + ctx.Request.QueryString);
            ctx.Response.Redirect($"/?returnUrl={returnUrl}");
            return Task.CompletedTask;
        };

        options.Events.OnRedirectToAccessDenied = ctx =>
        {
            if (IsApiRequest(ctx.Request))
            {
                ctx.Response.StatusCode = StatusCodes.Status403Forbidden; // API/AJAX -> 403
                return Task.CompletedTask;
            }

            ctx.Response.Redirect("/");
            return Task.CompletedTask;
        };
    });*/

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/";
    options.AccessDeniedPath = "/";
});

builder.Services.AddAuthorization(); // servidor

// ======  AUTORIZACIÓN DEL CLIENTE (para Razor Components)  ======
builder.Services.AddAuthorizationCore();

// ======  JWT en cliente ======
builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();   // Token storage (localStorage vía JS)
builder.Services.AddScoped<JwtAuthStateProvider>();                        // Auth state basado en JWT
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthStateProvider>());

// Handler que agrega Authorization: Bearer <token>
builder.Services.AddTransient<AuthHeaderHandler>();

// HttpClient hacia tu API con el handler del Bearer
builder.Services.AddHttpClient("Api", c =>
{
    //c.BaseAddress = new Uri("https://tu-api.com/"); // <-- CAMBIA A TU API (con '/' final)
    c.BaseAddress = new Uri("https://fakestoreapi.com/");
})
.AddHttpMessageHandler<AuthHeaderHandler>();

// HttpClient por defecto = cliente "Api"
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

// Servicio de login/logout
builder.Services.AddScoped<IAuthService, AuthService>();



var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseAuthentication();   // requerido por [Authorize] en componentes server
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddAdditionalAssemblies(typeof(MauiStore.Shared._Imports).Assembly);

app.Run();

static bool IsApiRequest(HttpRequest req)
{
    // Ajusta este prefijo si tu API usa otro (por ejemplo /api, /v1, etc.)
    if (req.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase))
        return true;

    // Heurística: si pide JSON explícitamente, trátalo como API
    var accept = req.Headers.Accept.ToString();
    if (!string.IsNullOrEmpty(accept) && accept.Contains("application/json", StringComparison.OrdinalIgnoreCase))
        return true;

    // Llamadas XHR/Fetch
    if (req.Headers.TryGetValue("X-Requested-With", out var v) &&
        string.Equals(v, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase))
        return true;

    return false;
}
