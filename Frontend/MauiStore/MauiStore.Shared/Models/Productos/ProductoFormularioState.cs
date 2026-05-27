using MauiStore.Shared.Models.Productos;

namespace MauiStore.Shared.Models.Productos;

public sealed class ProductoFormularioState
{
    public bool Cargando { get; set; }
    public bool Guardando { get; set; }
    public bool ConfirmacionVisible { get; set; }
    public bool EsFarmacia { get; set; }
    public string? Error { get; set; }
    public string? Exito { get; set; }

    public ProductoCreateRequest Formulario { get; set; } = new();

    public List<ProductoCatalogoItemDto> UnidadesMedida { get; set; } = new();
    public List<ProductoCatalogoItemDto> CondicionesDevolucion { get; set; } = new();
    public List<ProductoCatalogoItemDto> Sucursales { get; set; } = new();
    public List<ProductoCatalogoItemDto> Cabys { get; set; } = new();

    public List<string> SucursalesSeleccionadas { get; set; } = new();
    public List<string> ImagenesNombre { get; set; } = new();
}