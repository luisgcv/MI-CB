using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvDinamica
{
    public string DinamicasConsecutivo { get; set; } = null!;

    public string TipoDinamica { get; set; } = null!;

    public int IdSucursal { get; set; }

    public string IdProveedor { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaHasta { get; set; }

    public decimal? MontoTotal { get; set; }

    public virtual TblProvProveedore IdProveedorNavigation { get; set; } = null!;

    public virtual TblProvSucursale IdSucursalNavigation { get; set; } = null!;

    public virtual ICollection<TblProvDinamicasCobro> TblProvDinamicasCobros { get; set; } = new List<TblProvDinamicasCobro>();

    public virtual ICollection<TblProvDinamicasLinea> TblProvDinamicasLineas { get; set; } = new List<TblProvDinamicasLinea>();
}
