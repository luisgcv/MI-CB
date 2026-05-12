using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvSellout
{
    public string SelloutConsecutivo { get; set; } = null!;

    public int IdSucursal { get; set; }

    public string IdProveedor { get; set; } = null!;

    public DateTime FechaVigenciaInicio { get; set; }

    public DateTime FechaVigenciaHasta { get; set; }

    public virtual TblProvProveedore IdProveedorNavigation { get; set; } = null!;

    public virtual TblProvSucursale IdSucursalNavigation { get; set; } = null!;

    public virtual ICollection<TblProvSelloutLinea> TblProvSelloutLineas { get; set; } = new List<TblProvSelloutLinea>();
}
