using MauiStore.Shared.Models.Productos;
using Microsoft.AspNetCore.Components.Forms;
using MauiStore.Shared.Models.Productos;

namespace MauiStore.Shared.Services.Articulos;


public sealed class ProductoFormularioSessionService
{
    public ProductoCreateRequest? Formulario { get; private set; }
    public List<ImagenCapturada> Imagenes { get; private set; } = new(); // ← ya no IBrowserFile
    public List<string> SucursalesSeleccionadas { get; private set; } = new();

    public List<ProductoCatalogoItemDto> UnidadesMedida { get; private set; } = new();
    public List<ProductoCatalogoItemDto> CondicionesDevolucion { get; private set; } = new();
    public List<ProductoCatalogoItemDto> Sucursales { get; private set; } = new();
    public List<ProductoCatalogoItemDto> Cabys { get; private set; } = new();
    public bool EsFarmacia { get; private set; }

    public void Guardar(
        ProductoCreateRequest formulario,
        List<ImagenCapturada> imagenes,        // ← tipo cambiado
        List<string> sucursalesSeleccionadas,
        List<ProductoCatalogoItemDto> unidades,
        List<ProductoCatalogoItemDto> condiciones,
        List<ProductoCatalogoItemDto> sucursales,
        List<ProductoCatalogoItemDto> cabys,
        bool esFarmacia)
    {
        Formulario = formulario;
        Imagenes = imagenes;
        SucursalesSeleccionadas = sucursalesSeleccionadas;
        UnidadesMedida = unidades;
        CondicionesDevolucion = condiciones;
        Sucursales = sucursales;
        Cabys = cabys;
        EsFarmacia = esFarmacia;
    }

    public void Limpiar()
    {
        Formulario = null;
        Imagenes = new();
        SucursalesSeleccionadas = new();
    }
}