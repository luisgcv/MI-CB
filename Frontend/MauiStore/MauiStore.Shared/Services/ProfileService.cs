using System.Net.Http.Json;
using MauiStore.Shared.Models.Profile;
//using CbEnLinea.Api.Common.Models.Profile;
using MauiStore.Web.Services;

namespace MauiStore.Shared.Services;

public sealed class ProfileService : BaseApiService
{
    private const string ProfilePath = "profile";
    private const string ChangePasswordPath = "profile/change-password";

    private readonly JwtAuthStateProvider _auth;

    public string? LastError { get; private set; }

    public ProfileService(
        JwtAuthStateProvider auth,
        ITokenStorageService tokenStorageService)
        : base(tokenStorageService)
    {
        _auth = auth;
    }

    private async Task InitiAuthentication()
    {
        var token = await _auth.GetTokenAsync();
        if (token is not null)
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<ApiBaseResponse<GetProfileResponse>> GetProfileAsync(
        CancellationToken ct = default)
    {
        try
        {
            LastError = null;

            await InitHttpClientHeaders();
            await InitiAuthentication();

            var response = await _http.GetAsync(ProfilePath, ct);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content
                    .ReadFromJsonAsync<GetProfileResponse>(cancellationToken: ct);

                return ApiBaseResponse<GetProfileResponse>.Ok(data);
            }

            var msg = await response.Content.ReadAsStringAsync(ct);
            LastError = string.IsNullOrWhiteSpace(msg)
                ? response.ReasonPhrase
                : msg;

            return ApiBaseResponse<GetProfileResponse>.Failure(LastError);
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return ApiBaseResponse<GetProfileResponse>.Failure(ex.Message);
        }
    }

    public async Task<ApiBaseResponse> UpdateProfileAsync(
        UpdateProfileRequest request,
        CancellationToken ct = default)
    {
        try
        {
            LastError = null;

            await InitHttpClientHeaders();
            await InitiAuthentication();


            var response = await _http.SendAsync(new HttpRequestMessage
            {
                Content = JsonContent.Create(request),
                Headers =
                    {
                        { "Accept", "application/json" }
                    },
                Method = HttpMethod.Post,
                RequestUri = new Uri(_http.BaseAddress!, ProfilePath)
            });


            if (response.IsSuccessStatusCode)
            {
                return ApiBaseResponse.Ok();
            }

            var msg = await response.Content.ReadAsStringAsync(ct);
            LastError = string.IsNullOrWhiteSpace(msg)
                ? response.ReasonPhrase
                : msg;

            return ApiBaseResponse.Failure(LastError);
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return ApiBaseResponse.Failure(ex.Message);
        }
    }

    public async Task<ApiBaseResponse> ChangePasswordAsync(
        ChangePasswordRequest request,
        CancellationToken ct = default)
    {
        try
        {
            LastError = null;

            await InitHttpClientHeaders();
            await InitiAuthentication();


            var response = await _http.SendAsync(new HttpRequestMessage
            {
                Content = JsonContent.Create(request),
                Headers =
                    {
                        { "Accept", "application/json" }
                    },
                Method = HttpMethod.Post,
                RequestUri = new Uri(_http.BaseAddress!, ChangePasswordPath)
            });

            if (response.IsSuccessStatusCode)
            {
                return ApiBaseResponse.Ok();
            }

            var msg = await response.Content.ReadAsStringAsync(ct);
            LastError = string.IsNullOrWhiteSpace(msg)
                ? response.ReasonPhrase
                : msg;

            return ApiBaseResponse.Failure(LastError);
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return ApiBaseResponse.Failure(ex.Message);
        }
    }
}
