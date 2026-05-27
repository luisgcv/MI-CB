using System.Text.Json;
using System.Text.Json.Serialization;

namespace MauiStore.Shared.Models.Productos;

public sealed class ProductoCatalogoItemDto
{
    [JsonPropertyName("id")]
    public JsonElement Id { get; set; }

    [JsonPropertyName("nombre")]
    public string? Nombre { get; set; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; set; }

    [JsonIgnore]
    public string Valor => Id.ValueKind switch
    {
        JsonValueKind.String => Id.GetString() ?? string.Empty,
        JsonValueKind.Number => Id.TryGetInt64(out var l) ? l.ToString() : Id.GetDecimal().ToString(System.Globalization.CultureInfo.InvariantCulture),
        JsonValueKind.True => bool.TrueString,
        JsonValueKind.False => bool.FalseString,
        _ => string.Empty
    };

    [JsonIgnore]
    public string Texto => !string.IsNullOrWhiteSpace(Nombre) ? Nombre! : Descripcion ?? string.Empty;
}