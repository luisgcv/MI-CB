using System.Net.Http.Json;
using MauiStore.Shared.Models.PurchaseOrders;
using MauiStore.Web.Services;

namespace MauiStore.Shared.Services;

public sealed class PurchaseOrderService : BaseApiService
{
    private const string BasePath = "purchase-orders";

    private readonly JwtAuthStateProvider _auth;

    public string? LastError { get; private set; }

    public PurchaseOrderService(
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
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    /// <summary>Obtiene el listado de órdenes de compra del proveedor autenticado.</summary>
    public async Task<ApiBaseResponse<List<PurchaseOrderSummaryDto>>> GetOrdersAsync(
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
                    .ReadFromJsonAsync<List<PurchaseOrderSummaryDto>>(cancellationToken: ct);

                return ApiBaseResponse<List<PurchaseOrderSummaryDto>>.Ok(data!);
            }

            var msg = await response.Content.ReadAsStringAsync(ct);
            LastError = string.IsNullOrWhiteSpace(msg) ? response.ReasonPhrase : msg;
            return ApiBaseResponse<List<PurchaseOrderSummaryDto>>.Failure(LastError);
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return ApiBaseResponse<List<PurchaseOrderSummaryDto>>.Failure(ex.Message);
        }
    }

    /// <summary>Obtiene el detalle de una orden específica.</summary>
    public async Task<ApiBaseResponse<PurchaseOrderDetailDto>> GetOrderDetailAsync(
        string id,
        CancellationToken ct = default)
    {
        try
        {
            LastError = null;

            await InitHttpClientHeaders();
            await InitAuthentication();

            var response = await _http.GetAsync($"{BasePath}/{id}", ct);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content
                    .ReadFromJsonAsync<PurchaseOrderDetailDto>(cancellationToken: ct);

                return ApiBaseResponse<PurchaseOrderDetailDto>.Ok(data!);
            }

            var msg = await response.Content.ReadAsStringAsync(ct);
            LastError = string.IsNullOrWhiteSpace(msg) ? response.ReasonPhrase : msg;
            return ApiBaseResponse<PurchaseOrderDetailDto>.Failure(LastError);
        }
        catch (Exception ex)
        {
            LastError = ex.Message;
            return ApiBaseResponse<PurchaseOrderDetailDto>.Failure(ex.Message);
        }
    }
}