using System.Text.Json.Serialization;

namespace MauiStore.Shared.Models.Productos;

public sealed class ProductoListaItemDto
{
    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("estadoActual")]
    public ProductoEstadoResumenDto? EstadoActual { get; set; }

    [JsonPropertyName("fechaCreacion")]
    public DateTime? FechaCreacion { get; set; }

    [JsonPropertyName("imagenPrincipal")]
    public ProductoImagenDto? ImagenPrincipal { get; set; }

    [JsonPropertyName("sucursales")]
    public List<ProductoCatalogoItemDto> Sucursales { get; set; } = new();

    [JsonPropertyName("cabys")]
    public List<ProductoCatalogoItemDto> Cabys { get; set; } = new();
}

public sealed class ProductoDetalleDto
{
    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("idUnidadMedida")]
    [JsonConverter(typeof(FlexibleStringConverter))]
    public string? IdUnidadMedida { get; set; }

    [JsonPropertyName("unidadMedida")]
    public string? UnidadMedida { get; set; }

    [JsonPropertyName("idCondicionesDevolucion")]
    [JsonConverter(typeof(FlexibleStringConverter))]
    public string? IdCondicionesDevolucion { get; set; }

    [JsonPropertyName("condicionDevolucion")]
    public string? CondicionDevolucion { get; set; }

    [JsonPropertyName("idEstado")]
    public int? IdEstado { get; set; }

    [JsonPropertyName("estado")]
    public string? Estado { get; set; }

    [JsonPropertyName("proveedor")]
    public string? proveedor { get; set; }

    [JsonPropertyName("porcIva")]
    public decimal? PorcIva { get; set; }

    [JsonPropertyName("minDespacho")]
    public decimal? MinDespacho { get; set; }

    [JsonPropertyName("embalaje")]
    public decimal? Embalaje { get; set; }

    [JsonPropertyName("gramaje")]
    public decimal? Gramaje { get; set; }

    [JsonPropertyName("devuelveMuestras")]
    public bool? DevuelveMuestras { get; set; }

    [JsonPropertyName("deseaPublicidad")]
    public bool? DeseaPublicidad { get; set; }

    [JsonPropertyName("aceptaDevolucion")]
    public bool? AceptaDevolucion { get; set; }

    [JsonPropertyName("tienePoliticaCambios")]
    public bool? TienePoliticaCambios { get; set; }

    [JsonPropertyName("condicionPago")]
    public int? CondicionPago { get; set; }

    [JsonPropertyName("costoSinIva")]
    public decimal? CostoSinIva { get; set; }

    [JsonPropertyName("costoConIva")]
    public decimal? CostoConIva { get; set; }

    [JsonPropertyName("descuentoIntroduccion")]
    public decimal? DescuentoIntroduccion { get; set; }

    [JsonPropertyName("descuentoEspecial")]
    public decimal? DescuentoEspecial { get; set; }

    [JsonPropertyName("descuentoPermanente")]
    public decimal? DescuentoPermanente { get; set; }

    [JsonPropertyName("margenSugerido")]
    public decimal? MargenSugerido { get; set; }

    [JsonPropertyName("alto")]
    public decimal? Alto { get; set; }

    [JsonPropertyName("ancho")]
    public decimal? Ancho { get; set; }

    [JsonPropertyName("profundidad")]
    public decimal? Profundidad { get; set; }

    [JsonPropertyName("fechaCreacion")]
    public DateTime? FechaCreacion { get; set; }

    [JsonPropertyName("sucursales")]
    public List<ProductoCatalogoItemDto> Sucursales { get; set; } = new();

    [JsonPropertyName("cabys")]
    public List<ProductoCabysDetalleDto> Cabys { get; set; } = new();

    [JsonPropertyName("imagenes")]
    public List<ProductoImagenDto> Imagenes { get; set; } = new();
}

public sealed class ProductoHistorialResponseDto
{
    [JsonPropertyName("articulo")]
    public ProductoListaItemDto? Articulo { get; set; }

    [JsonPropertyName("detalle")]
    public ProductoHistorialDetalleDto? Detalle { get; set; }

    [JsonPropertyName("estadoActual")]
    public ProductoEstadoResumenDto? EstadoActual { get; set; }

    [JsonPropertyName("historial")]
    public List<ProductoHistorialEventoDto> Historial { get; set; } = new();
}

public sealed class ProductoHistorialDetalleDto
{
    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("idProveedor")]
    public string? IdProveedor { get; set; }

    [JsonPropertyName("fechaCreacion")]
    public DateTime? FechaCreacion { get; set; }

    [JsonPropertyName("sucursales")]
    public List<ProductoCatalogoItemDto> Sucursales { get; set; } = new();

    [JsonPropertyName("cabys")]
    public List<ProductoCabysDetalleDto> Cabys { get; set; } = new();
}

public sealed class ProductoHistorialEventoDto
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("estado")]
    public ProductoEstadoResumenDto? Estado { get; set; }

    [JsonPropertyName("motivo")]
    public ProductoMotivoResumenDto? Motivo { get; set; }

    [JsonPropertyName("comentario")]
    public string? Comentario { get; set; }

    [JsonPropertyName("fecha")]
    public DateTime? Fecha { get; set; }
}

public sealed class ProductoEstadoResumenDto
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("nombre")]
    public string? Nombre { get; set; }
}

public sealed class ProductoMotivoResumenDto
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; set; }
}

public sealed class ProductoImagenDto
{
    [JsonPropertyName("nombreArchivo")]
    public string? NombreArchivo { get; set; }

    [JsonPropertyName("archivo")]
    public string? Archivo { get; set; }
}

public sealed class ProductoCabysDetalleDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("nombre")]
    public string? Nombre { get; set; }

    [JsonPropertyName("formaFarmaceutica")]
    public string? FormaFarmaceutica { get; set; }

    [JsonPropertyName("registroMedicamento")]
    public string? RegistroMedicamento { get; set; }
}
