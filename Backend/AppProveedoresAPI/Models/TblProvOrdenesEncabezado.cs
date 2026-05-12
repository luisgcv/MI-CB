using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvOrdenesEncabezado
{
    public string IdOrden { get; set; } = null!;

    public int IdSucursal { get; set; }

    public string? IdProveedor { get; set; }

    public int IdEstadoOrden { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaRequerida { get; set; }

    public string NombreCreador { get; set; } = null!;

    public decimal MontoTotal { get; set; }

    public virtual TblProvEstadoOrdene IdEstadoOrdenNavigation { get; set; } = null!;

    public virtual TblProvProveedore? IdProveedorNavigation { get; set; }

    public virtual TblProvSucursale IdSucursalNavigation { get; set; } = null!;

    public virtual ICollection<TblProvOrdenesLinea> TblProvOrdenesLineas { get; set; } = new List<TblProvOrdenesLinea>();
}
