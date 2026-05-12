using System;
using System.Collections.Generic;

namespace AppProveedoresAPI.Models;

public partial class TblProvEstadosProducto
{
    public int IdEstado { get; set; }

    public string DescripcionEstado { get; set; } = null!;

    public virtual ICollection<TblProvProducto> TblProvProductos { get; set; } = new List<TblProvProducto>();

    public virtual ICollection<TblProvProductosEstado> TblProvProductosEstados { get; set; } = new List<TblProvProductosEstado>();
}
