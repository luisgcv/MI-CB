namespace MauiStore.Shared.Models.Productos;

public sealed class ProductoCreateRequest
{
    public string? IdSku { get; set; }
    public string? Descripcion { get; set; }
    public string? IdUnidadMedida { get; set; }
    public string? IdCondicionesDevolucion { get; set; }
    public decimal? PorcIva { get; set; }
    public decimal? MinDespacho { get; set; }
    public decimal? Embalaje { get; set; }
    public decimal? Gramaje { get; set; }
    public bool? DevuelveMuestras { get; set; }
    public bool? DeseaPublicidad { get; set; }
    public bool? AceptaDevolucion { get; set; }
    public bool? TienePoliticaCambios { get; set; }
    public int? CondicionPago { get; set; }
    public decimal? CostoSinIva { get; set; }
    public decimal? CostoConIva { get; set; }
    public decimal? DescuentoIntroduccion { get; set; }
    public decimal? DescuentoEspecial { get; set; }
    public decimal? DescuentoPermanente { get; set; }
    public decimal? MargenSugerido { get; set; }
    public decimal? Alto { get; set; }
    public decimal? Ancho { get; set; }
    public decimal? Profundidad { get; set; }
    public List<string> IdSucursales { get; set; } = new();
    public string? IdCabys { get; set; }
    public string? FormaFarmaceutica { get; set; }
    public string? RegistroMedicamento { get; set; }
}