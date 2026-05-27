namespace MauiStore.Shared.Models.Productos;

public sealed class ImagenCapturada
{
    public string Name { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
    public byte[] Data { get; init; } = Array.Empty<byte>();
}