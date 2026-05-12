// MauiStore/Services/MauiTokenStorageService.cs
using Microsoft.Maui.Storage;

namespace MauiStore.Web.Services   // usa el mismo namespace donde está ITokenStorageService
{
    public sealed class MauiTokenStorageService : ITokenStorageService
    {
        private const string Key = "auth_token";

        public async Task SetTokenAsync(string? token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    SecureStorage.Default.Remove(Key);
                else
                    await SecureStorage.Default.SetAsync(Key, token);
            }
            catch { }
        }

        public async Task<string?> GetTokenAsync()
        {
            try { return await SecureStorage.Default.GetAsync(Key); }
            catch { return null; }
        }

        public Task ClearAsync()
        {
            try { SecureStorage.Default.Remove(Key); } catch { }
            return Task.CompletedTask;
        }
        public async Task<string> GetClientId()
        {
            const string clientIdKey = "client_id";
            try
            {
                var clientId = await SecureStorage.Default.GetAsync(clientIdKey);
                if (string.IsNullOrEmpty(clientId))
                {
                    clientId = Guid.NewGuid().ToString();
                    await SecureStorage.Default.SetAsync(clientIdKey, clientId);
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
