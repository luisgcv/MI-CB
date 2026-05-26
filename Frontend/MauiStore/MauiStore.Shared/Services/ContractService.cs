using MauiStore.Shared.Dtos;
using MauiStore.Web.Services;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MauiStore.Shared.Services;

public sealed class ContractService : BaseApiService
{
    private const string ContractsPath = "contracts";

    public ContractService(ITokenStorageService tokenStorageService)
        : base(tokenStorageService)
    {
    }

    public async Task<List<ContractSummaryDto>> GetContractsAsync(CancellationToken cancellationToken = default)
    {
        await InitHttpClientHeaders();

        var response = await _http.GetAsync(ContractsPath, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new List<ContractSummaryDto>();

            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"Error loading contracts ({response.StatusCode}): {body}");
        }

        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        return await response.Content.ReadFromJsonAsync<List<ContractSummaryDto>>(options, cancellationToken: cancellationToken)
               ?? new List<ContractSummaryDto>();
    }

    public async Task DownloadPdfAsync(string consecutivo, IJSRuntime js, CancellationToken cancellationToken = default)
    {
        await InitHttpClientHeaders();

        var response = await _http.GetAsync($"{ContractsPath}/{consecutivo}/pdf", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"Error downloading PDF ({response.StatusCode}): {body}");
        }

        var bytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        var base64 = Convert.ToBase64String(bytes);
        await js.InvokeVoidAsync("downloadPdf", base64, $"contrato-{consecutivo}.pdf");
    }
}