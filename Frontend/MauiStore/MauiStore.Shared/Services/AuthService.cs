// /MauiStore.Web/Services/AuthService.cs
using System.Net.Http.Json;
using System.Text.Json;
using CbEnLinea.Api.Common.Dtos;
using MauiStore.Infrastructure;
using MauiStore.Shared.Models.Auth;
using MauiStore.Shared.Services;
using MauiStore.Web.Services;

namespace MauiStore.Shared.Services
{
    public interface IAuthService
    {
        Task<(bool ok, string? error, TokenResponse? data)> LoginAsync(string identifier, string password, CancellationToken ct = default);
        Task<(bool ok, string? error, RegisterResponse? data)> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
        Task<(bool ok, string? error, ForgotPasswordResponse? data)> ForgotPasswordAsync(string email, CancellationToken ct = default);
        Task LogoutAsync();
        string? LastError { get; }
    }

    public sealed class AuthService : BaseApiService, IAuthService
    {
        private const string TokenPath = "auth/token";                 // POST
        private const string RegisterPath = "auth/register";           // POST
        private const string ForgotPasswordPath = "auth/forgot-password"; // POST

        private readonly JwtAuthStateProvider _auth;
        public string? LastError { get; private set; }


        public AuthService(JwtAuthStateProvider auth, ITokenStorageService tokenStorageService) : base(tokenStorageService)
        {
            _auth = auth;
        }

        // ---------- Login ----------
        public Task<(bool ok, string? error, TokenResponse? data)> LoginAsync(string identifier, string password, CancellationToken ct = default)
            => LoginAsync(new TokenRequest
            {
                Identifier = identifier?.Trim() ?? string.Empty,
                Password = password?.Trim() ?? string.Empty
            }, ct);

        public async Task<(bool ok, string? error, TokenResponse? data)> LoginAsync(TokenRequest request, CancellationToken ct = default)
        {
            try
            {
                await InitHttpClientHeaders();
                var resp = await _http.PostAsJsonAsync(TokenPath, request, ct);
                if (!resp.IsSuccessStatusCode)
                {
                    var msg = await resp.Content.ReadAsStringAsync(ct);
                    return (false, string.IsNullOrWhiteSpace(msg) ? resp.ReasonPhrase : msg, null);
                }

                var data = await resp.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: ct);
                await _auth.MarkUserAsAuthenticatedAsync(data!.AccessToken);
                return (true, null, data);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        // ---------- Register ----------
        public async Task<(bool ok, string? error, RegisterResponse? data)> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
        {
            try
            {
                await InitHttpClientHeaders();
                var resp = await _http.PostAsJsonAsync(RegisterPath, request, ct);
                if (!resp.IsSuccessStatusCode)
                {
                    var msg = await resp.Content.ReadAsStringAsync(ct);
                    return (false, string.IsNullOrWhiteSpace(msg) ? resp.ReasonPhrase : msg, null);
                }

                var data = await resp.Content.ReadFromJsonAsync<RegisterResponse>(cancellationToken: ct);
                await _auth.MarkUserAsAuthenticatedAsync(data!.AccessToken!);
                return (true, null, data);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        // ---------- Forgot Password ----------
        public async Task<(bool ok, string? error, ForgotPasswordResponse? data)> ForgotPasswordAsync(string email, CancellationToken ct = default)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync(ForgotPasswordPath, new ForgotPasswordRequest
                {
                    Email = email,
                    Target = LinkTarget.Web,
                }, ct);
                if (!resp.IsSuccessStatusCode)
                {
                    var msg = await resp.Content.ReadAsStringAsync(ct);
                    return (false, string.IsNullOrWhiteSpace(msg) ? resp.ReasonPhrase : msg, null);
                }

                var data = await resp.Content.ReadFromJsonAsync<ForgotPasswordResponse>(cancellationToken: ct);
                return (true, null, data);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        // ---------- Logout ----------
        public async Task LogoutAsync() => await _auth.MarkUserAsLoggedOutAsync();
    }
}
