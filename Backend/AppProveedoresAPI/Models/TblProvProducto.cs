using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvProducto
{
    public string IdSku { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int IdUnidadMedida { get; set; }

    public int IdCondicionesDevolucion { get; set; }

    public int IdEstado { get; set; }

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

    public DateTime? FechaCreacion { get; set; }

    public virtual TblProvCondicionesDevolucion IdCondicionesDevolucionNavigation { get; set; } = null!;

    public virtual TblProvEstadosProducto IdEstadoNavigation { get; set; } = null!;

    public virtual TblProvUnidadesMedidum IdUnidadMedidaNavigation { get; set; } = null!;

    public virtual ICollection<TblProvProductosCaby> TblProvProductosCabies { get; set; } = new List<TblProvProductosCaby>();

    public virtual ICollection<TblProvProductosEstado> TblProvProductosEstados { get; set; } = new List<TblProvProductosEstado>();

    public virtual ICollection<TblProvProductosImagene> TblProvProductosImagenes { get; set; } = new List<TblProvProductosImagene>();

    public virtual ICollection<TblProvSucursale> IdSucursals { get; set; } = new List<TblProvSucursale>();
}
