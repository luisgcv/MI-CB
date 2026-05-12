using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvOrdenesLinea
{
    public string IdOrden { get; set; } = null!;

    public string SkuOrden { get; set; } = null!;

    public string SkuOrdenDescripcion { get; set; } = null!;

    public decimal? Costo { get; set; }

    public decimal? CantidadOrdenada { get; set; }

    public decimal? CantidadRecibida { get; set; }

    public decimal? CantidadDevolucion { get; set; }

    public decimal? CantidadBackorder { get; set; }

    public virtual TblProvOrdenesEncabezado IdOrdenNavigation { get; set; } = null!;
}
