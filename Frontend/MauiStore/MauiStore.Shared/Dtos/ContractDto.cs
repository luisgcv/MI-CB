using System.Text.Json.Serialization;

namespace MauiStore.Shared.Dtos;

public class ContractSummaryDto
{
    [JsonPropertyName("consecutivo")]
    public string Consecutivo { get; set; } = string.Empty;

    [JsonPropertyName("tipoContrato")]
    public string TipoContrato { get; set; } = string.Empty;

    [JsonPropertyName("sucursal")]
    public string Sucursal { get; set; } = string.Empty;

    [JsonPropertyName("fechaInicio")]
    public DateTime FechaInicio { get; set; }

    [JsonPropertyName("fechaHasta")]
    public DateTime FechaHasta { get; set; }

    [JsonPropertyName("montoMensual")]
    public decimal MontoMensual { get; set; }

    [JsonPropertyName("estado")]
    public string Estado { get; set; } = string.Empty;

    [JsonPropertyName("diasRestantes")]
    public int DiasRestantes { get; set; }

    [JsonPropertyName("porcentajeTranscurrido")]
    public int PorcentajeTranscurrido { get; set; }
}