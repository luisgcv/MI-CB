using System.Net.Http.Json;
using MauiStore.Shared.Models.AccountStatements;
using MauiStore.Web.Services;
using Microsoft.JSInterop;

namespace MauiStore.Shared.Services;

public sealed class AccountStatementService : BaseApiService
{
    private const string BasePath = "account-statements";
    private readonly JwtAuthStateProvider _auth;

    public AccountStatementService(
        JwtAuthStateProvider auth,
        ITokenStorageService tokenStorageService)
        : base(tokenStorageService)
    {
        _auth = auth;
    }

    public async Task<ApiBaseResponse<AccountStatementResultDto>> GetStatementsAsync(
        DateTime? from = null,
        DateTime? to = null,
        CancellationToken ct = default)
    {
        try
        {
            await InitHttpClientHeaders();

            var url = BasePath;
            if (from.HasValue && to.HasValue)
                url += $"?from={from.Value:yyyy-MM-dd}&to={to.Value:yyyy-MM-dd}";

            var response = await _http.GetAsync(url, ct);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content
                    .ReadFromJsonAsync<AccountStatementResultDto>(cancellationToken: ct);
                return ApiBaseResponse<AccountStatementResultDto>.Ok(data!);
            }

            var msg = await response.Content.ReadAsStringAsync(ct);
            return ApiBaseResponse<AccountStatementResultDto>.Failure(msg);
        }
        catch (Exception ex)
        {
            return ApiBaseResponse<AccountStatementResultDto>.Failure(ex.Message);
        }
    }

    public async Task DownloadPdfAsync(
        IJSRuntime js,
        DateTime? from = null,
        DateTime? to = null,
        CancellationToken ct = default)
    {
        await InitHttpClientHeaders();

        var url = $"{BasePath}/pdf";
        if (from.HasValue && to.HasValue)
            url += $"?from={from.Value:yyyy-MM-dd}&to={to.Value:yyyy-MM-dd}";

        var response = await _http.GetAsync(url, ct);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException($"Error al descargar PDF ({response.StatusCode}): {body}");
        }

        var bytes = await response.Content.ReadAsByteArrayAsync(ct);
        var base64 = Convert.ToBase64String(bytes);
        await js.InvokeVoidAsync("downloadPdf", base64, "estado-de-cuenta.pdf");
    }

    public async Task DownloadExcelAsync(
        IJSRuntime js,
        DateTime? from = null,
        DateTime? to = null,
        CancellationToken ct = default)
    {
        await InitHttpClientHeaders();

        var url = $"{BasePath}/excel";
        if (from.HasValue && to.HasValue)
            url += $"?from={from.Value:yyyy-MM-dd}&to={to.Value:yyyy-MM-dd}";

        var response = await _http.GetAsync(url, ct);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException($"Error al descargar Excel ({response.StatusCode}): {body}");
        }

        var bytes = await response.Content.ReadAsByteArrayAsync(ct);
        var base64 = Convert.ToBase64String(bytes);
        await js.InvokeVoidAsync("downloadExcel", base64, "estado-de-cuenta.xlsx");
    }
}