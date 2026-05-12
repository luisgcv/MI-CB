using System.Net.Http.Headers;

namespace MauiStore.Web.Services
{
    /// <summary>
    /// Agrega Authorization: Bearer &lt;token&gt; a cada request del HttpClient.
    /// </summary>
    public sealed class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ITokenStorageService _storage;

        public AuthHeaderHandler(ITokenStorageService storage) => _storage = storage;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _storage.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}