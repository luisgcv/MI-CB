using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using MauiStore.Shared.Models.Productos;
using MauiStore.Web.Services;

namespace MauiStore.Shared.Services.Articulos;

public sealed class ProductoService : BaseApiService
{
    private const string BasePath = "productos";

    public ProductoService(ITokenStorageService tokenStorageService)
        : base(tokenStorageService)
    {
    }

    public Task<List<ProductoCatalogoItemDto>> GetUnidadesMedidaAsync(CancellationToken ct = default) =>
        GetCatalogAsync($"{BasePath}/unidades-medida", ct);

    public Task<List<ProductoCatalogoItemDto>> GetCondicionesDevolucionAsync(CancellationToken ct = default) =>
        GetCatalogAsync($"{BasePath}/condiciones-devolucion", ct);

    public Task<List<ProductoCatalogoItemDto>> GetSucursalesAsync(CancellationToken ct = default) =>
        GetCatalogAsync($"{BasePath}/sucursales", ct);

    public Task<List<ProductoCatalogoItemDto>> GetCabysAsync(CancellationToken ct = default) =>
        GetCatalogAsync($"{BasePath}/cabys", ct);

    public async Task<bool> CreateProductoAsync(ProductoCreateRequest request, IReadOnlyList<ImagenCapturada> imagenes, CancellationToken ct = default)
    {
        await InitHttpClientHeaders();

        using var content = new MultipartFormDataContent();
        AddString(content, "idSku", request.IdSku);
        AddString(content, "descripcion", request.Descripcion);
        AddString(content, "idUnidadMedida", request.IdUnidadMedida);
        AddString(content, "idCondicionesDevolucion", request.IdCondicionesDevolucion);
        AddDecimal(content, "porcIva", request.PorcIva);
        AddDecimal(content, "minDespacho", request.MinDespacho);
        AddInt(content, "condicionPago", request.CondicionPago);
        AddDecimal(content, "gramaje", request.Gramaje);
        AddBool(content, "devuelveMuestras", request.DevuelveMuestras);
        AddBool(content, "deseaPublicidad", request.DeseaPublicidad);
        AddBool(content, "aceptaDevolucion", request.AceptaDevolucion);
        AddBool(content, "tienePoliticaCambios", request.TienePoliticaCambios);
        AddDecimal(content, "costoSinIva", request.CostoSinIva);
        AddDecimal(content, "costoConIva", request.CostoConIva);
        AddDecimal(content, "descuentoIntroduccion", request.DescuentoIntroduccion);
        AddDecimal(content, "descuentoEspecial", request.DescuentoEspecial);
        AddDecimal(content, "descuentoPermanente", request.DescuentoPermanente);
        AddDecimal(content, "margenSugerido", request.MargenSugerido);
        AddDecimal(content, "alto", request.Alto);
        AddDecimal(content, "ancho", request.Ancho);
        AddDecimal(content, "profundidad", request.Profundidad);
        foreach (var idSucursal in request.IdSucursales)
        {
            content.Add(new StringContent(idSucursal), "idSucursales");
        }
        AddString(content, "idCabys", request.IdCabys);
        AddString(content, "formaFarmaceutica", request.FormaFarmaceutica);
        AddString(content, "registroMedicamento", request.RegistroMedicamento);
        foreach (var imagen in imagenes.Take(2))
        {
            var fileContent = new ByteArrayContent(imagen.Data);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(imagen.ContentType);
            content.Add(fileContent, "imagenes", imagen.Name);
        }

        using var response = await _http.PostAsync(BasePath, content, ct);
        if (response.IsSuccessStatusCode) return true;
        var error = await response.Content.ReadAsStringAsync(ct);
        throw new HttpRequestException($"Error creando producto ({response.StatusCode}): {error}");
    }

    private static void AddString(MultipartFormDataContent content, string name, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            content.Add(new StringContent(value), name);
        }
    }

    private static void AddBool(MultipartFormDataContent content, string name, bool? value)
    {
        if (value is not null)
        {
            content.Add(new StringContent(value.Value ? "1" : "0"), name);
        }
    }

    private static void AddInt(MultipartFormDataContent content, string name, int? value)
    {
        if (value is not null)
            content.Add(new StringContent(value.Value.ToString()), name);
    }
    private static void AddDecimal(MultipartFormDataContent content, string name, decimal? value)
    {
        if (value is not null)
        {
            content.Add(new StringContent(value.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)), name);
        }
    }

    private async Task<List<ProductoCatalogoItemDto>> GetCatalogAsync(string path, CancellationToken ct)
    {
        await InitHttpClientHeaders();
        using var response = await _http.GetAsync(path, ct);
        if (!response.IsSuccessStatusCode) return new();
        return await response.Content.ReadFromJsonAsync<List<ProductoCatalogoItemDto>>(cancellationToken: ct) ?? new();
    }
}