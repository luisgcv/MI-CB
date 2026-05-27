using System.Net.Http.Json;
using MauiStore.Shared.Models.Dynamics;
using MauiStore.Web.Services;

namespace MauiStore.Shared.Services
{
    public sealed class DynamicsService : BaseApiService
    {
        private const string BasePath = "dynamics";

        private readonly JwtAuthStateProvider _auth;

        public string? LastError { get; private set; }

        public DynamicsService(
            JwtAuthStateProvider auth,
            ITokenStorageService tokenStorageService)
            : base(tokenStorageService)
        {
            _auth = auth;
        }

        private async Task InitAuthentication()
        {
            var token = await _auth.GetTokenAsync();

            if (token is not null)
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        token);
            }
        }

        public async Task<ApiBaseResponse<List<DynamicSummaryDto>>> GetDynamicsAsync(
            CancellationToken ct = default)
        {
            try
            {
                LastError = null;

                await InitHttpClientHeaders();
                await InitAuthentication();

                var response = await _http.GetAsync(BasePath, ct);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content
                        .ReadFromJsonAsync<List<DynamicSummaryDto>>(
                            cancellationToken: ct);

                    return ApiBaseResponse<List<DynamicSummaryDto>>.Ok(data!);
                }

                var msg = await response.Content.ReadAsStringAsync(ct);

                LastError = string.IsNullOrWhiteSpace(msg)
                    ? response.ReasonPhrase
                    : msg;

                return ApiBaseResponse<List<DynamicSummaryDto>>
                    .Failure(LastError);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;

                return ApiBaseResponse<List<DynamicSummaryDto>>
                    .Failure(ex.Message);
            }
        }

        public async Task<ApiBaseResponse<DynamicDetailDto>> GetDynamicDetailAsync(
            string id,
            CancellationToken ct = default)
        {
            try
            {
                LastError = null;

                await InitHttpClientHeaders();
                await InitAuthentication();

                var response = await _http.GetAsync(
                    $"{BasePath}/{id}",
                    ct);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content
                        .ReadFromJsonAsync<DynamicDetailDto>(
                            cancellationToken: ct);

                    return ApiBaseResponse<DynamicDetailDto>.Ok(data!);
                }

                var msg = await response.Content.ReadAsStringAsync(ct);

                LastError = string.IsNullOrWhiteSpace(msg)
                    ? response.ReasonPhrase
                    : msg;

                return ApiBaseResponse<DynamicDetailDto>
                    .Failure(LastError);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;

                return ApiBaseResponse<DynamicDetailDto>
                    .Failure(ex.Message);
            }
        }
    }
}