// /MauiStore.Web/Services/AuthService.cs

using System.Net.Http.Json;
using CbEnLinea.Api.Common.Dtos;
using MauiStore.Infrastructure;
using MauiStore.Shared.Models.Auth;
using MauiStore.Web.Services;

namespace MauiStore.Shared.Services
{
    public interface IAuthService
    {
        Task<(bool ok, string? error, LoginResponse? data)> LoginAsync(
            string identifier,
            string password,
            CancellationToken ct = default);

        Task<(bool ok, string? error, RegisterResponse? data)> RegisterAsync(
            RegisterRequest request,
            CancellationToken ct = default);

        Task<(bool ok, string? error, ForgotPasswordResponse? data)> ForgotPasswordAsync(
            string email,
            CancellationToken ct = default);

        Task LogoutAsync();

        string? LastError { get; }
    }

    public sealed class AuthService : BaseApiService, IAuthService
    {
        private const string LoginPath = "auth/login";
        private const string RegisterPath = "auth/register";
        private const string ForgotPasswordPath = "auth/forgot-password";

        private readonly JwtAuthStateProvider _auth;

        public string? LastError { get; private set; }

        public AuthService(
            JwtAuthStateProvider auth,
            ITokenStorageService tokenStorageService)
            : base(tokenStorageService)
        {
            _auth = auth;
        }

        // ================= LOGIN =================

        public async Task<(bool ok, string? error, LoginResponse? data)>
            LoginAsync(
                string identifier,
                string password,
                CancellationToken ct = default)
        {
            try
            {
                await InitHttpClientHeaders();

                // Body EXACTO que espera NestJS
                var requestBody = new
                {
                    identificationId = identifier.Trim(),
                    password = password.Trim()
                };

                var resp = await _http.PostAsJsonAsync(
                    LoginPath,
                    requestBody,
                    ct);

                // ERROR HTTP
                if (!resp.IsSuccessStatusCode)
                {
                    var errorContent = await resp.Content.ReadAsStringAsync(ct);
                    if (errorContent.Contains("Usuario no existe"))
                    {
                        return (false, "El usuario no existe.", null);
                    }

                    if (errorContent.Contains("Unauthorized"))
                    {
                        return (false, "Usuario o contraseña incorrectos.", null);
                    }

                    return (
                        false,
                        "No fue posible iniciar sesión.",
                        null
                    );
                }

                // RESPUESTA
                var data = await resp.Content.ReadFromJsonAsync<LoginResponse>(
                    cancellationToken: ct);

                if (data == null || string.IsNullOrWhiteSpace(data.Token))
                {
                    return (false, "Token inválido", null);
                }

                // GUARDAR TOKEN
                await _tokenStorageService.SetTokenAsync(data.Token);

                // ACTUALIZAR AUTH STATE
                await _auth.MarkUserAsAuthenticatedAsync(data.Token);

                return (true, null, data);
            }
            catch (Exception ex)
            {
                return (false, ex.ToString(), null);
            }
        }

        // ================= REGISTER =================

        public async Task<(bool ok, string? error, RegisterResponse? data)>
            RegisterAsync(
                RegisterRequest request,
                CancellationToken ct = default)
        {
            try
            {
                await InitHttpClientHeaders();

                var resp = await _http.PostAsJsonAsync(
                    RegisterPath,
                    request,
                    ct);

                if (!resp.IsSuccessStatusCode)
                {
                    var msg = await resp.Content.ReadAsStringAsync(ct);

                    return (
                        false,
                        string.IsNullOrWhiteSpace(msg)
                            ? resp.ReasonPhrase
                            : msg,
                        null
                    );
                }

                var data = await resp.Content
                    .ReadFromJsonAsync<RegisterResponse>(
                        cancellationToken: ct);

                if (data?.AccessToken != null)
                {
                    await _auth.MarkUserAsAuthenticatedAsync(data.AccessToken);
                }

                return (true, null, data);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        // ================= FORGOT PASSWORD =================

        public async Task<(bool ok, string? error, ForgotPasswordResponse? data)>
            ForgotPasswordAsync(
                string email,
                CancellationToken ct = default)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync(
                    ForgotPasswordPath,
                    new ForgotPasswordRequest
                    {
                        Email = email,
                        Target = LinkTarget.Web,
                    },
                    ct);

                if (!resp.IsSuccessStatusCode)
                {
                    var msg = await resp.Content.ReadAsStringAsync(ct);

                    return (
                        false,
                        string.IsNullOrWhiteSpace(msg)
                            ? resp.ReasonPhrase
                            : msg,
                        null
                    );
                }

                var data = await resp.Content
                    .ReadFromJsonAsync<ForgotPasswordResponse>(
                        cancellationToken: ct);

                return (true, null, data);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        // ================= LOGOUT =================

        public async Task LogoutAsync()
        {
            await _tokenStorageService.ClearAsync();
            await _auth.MarkUserAsLoggedOutAsync();
        }
    }
}