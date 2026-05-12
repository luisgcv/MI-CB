// MauiStore.Web/Services/TokenStorageService.cs
using Microsoft.JSInterop;

namespace MauiStore.Web.Services
{
    public interface ITokenStorageService
    {
        Task SetTokenAsync(string? token);
        Task<string?> GetTokenAsync();
        Task<string> GetClientId();
        Task ClearAsync();
    }

    public sealed class TokenStorageService : ITokenStorageService
    {
        private const string Key = "auth_token";   // <—  ¡nombre único y consistente!
        private readonly IJSRuntime _js;
        public TokenStorageService(IJSRuntime js) => _js = js;

        public async Task SetTokenAsync(string? token)
        {
            try { await _js.InvokeVoidAsync("localStorage.setItem", Key, token ?? ""); }
            catch { /* prerender / disconnect: ignorar */ }
        }

        public async Task<string?> GetTokenAsync()
        {
            try { return await _js.InvokeAsync<string?>("localStorage.getItem", Key); }
            catch { return null; }
        }

        public async Task ClearAsync()
        {
            try { await _js.InvokeVoidAsync("localStorage.removeItem", Key); }
            catch { /* prerender / disconnect: ignorar */ }
        }

        public async Task<string> GetClientId()
        {
            try
            {
                var clientId = await _js.InvokeAsync<string?>("localStorage.getItem", "client_id");
                if (string.IsNullOrEmpty(clientId))
                {
                    clientId = Guid.NewGuid().ToString();
                    await _js.InvokeVoidAsync("localStorage.setItem", "client_id", clientId);
                }
                return clientId;
            }
            catch
            {
                return Guid.NewGuid().ToString();
            }
        }
    }
}

