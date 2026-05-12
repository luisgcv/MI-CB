using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvEstadoOrdene
{
    public int IdEstadoOrden { get; set; }

    public string? DescripcionOrden { get; set; }
    
    public virtual ICollection<TblProvOrdenesEncabezado> TblProvOrdenesEncabezados { get; set; } = new List<TblProvOrdenesEncabezado>();
}
